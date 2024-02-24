namespace SQLRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

public class SQLUserRepository
{
    private readonly string connectionString;
    public SQLUserRepository()
    {
        IConfiguration configuration = ConfigurationHelper.GetConfiguration();
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public List<User> GetUsers()
    {
        List<User> users = new List<User>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM mstUser";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["UserName"],
                            Email = (string)reader["Email"],
                            PasswordHash = (string)reader["PasswordHash"]
                        };
                        users.Add(user);
                    }
                }
            }
        }
        return users;
    }
}

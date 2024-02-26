namespace SQLRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using MyUtility;
using Models;

public class SQLUserRepository
{
    private string connectionString = string.Empty;
    public SQLUserRepository()
    {
        connectionString = ConfigurationHelper.Instance.GetConnectionString();
    }
    public List<User> GetUsers()
    {
        List<User> users = new List<User>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string query = "SELECT * FROM mstUser";

            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
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

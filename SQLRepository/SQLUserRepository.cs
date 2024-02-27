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
							Username = (string)reader["Username"],
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
	public bool AddUser(User user)
	{
		bool success = false;
		try
		{
			string hashedPassword = HashPassword(user.PasswordHash);

			using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				connection.Open();

				string query = "INSERT INTO mstUser (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Username", user.Username);
					command.Parameters.AddWithValue("@Email", user.Email);
					command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

					int rowsAffected = command.ExecuteNonQuery();
					success = rowsAffected > 0;
				}
			}
		}
		catch (Exception ex)
		{
			throw;
		}

		return success;
	}

	private string HashPassword(string password)
	{
		// Generate a salt and hash the password using bcrypt
		return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
	}
}

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
				}
			}
		}
		catch (Exception ex)
		{
			throw;
		}

		return true;
	}

	private string HashPassword(string password)
	{
		// Generate a salt and hash the password using bcrypt
		// return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
		return BCrypt.Net.BCrypt.HashPassword(password);

	}

	public bool VerifyPassword(string providedPassword, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
	}

	// public static string EncodePasswordToBase64(string password)
	// {
	// 	try
	// 	{
	// 		byte[] encData_byte = new byte[password.Length];
	// 		encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
	// 		string encodedData = Convert.ToBase64String(encData_byte);
	// 		return encodedData;
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		throw new Exception("Error in base64Encode" + ex.Message);
	// 	}
	// }

	// //this function Convert to Decord your Password
	// public string DecodeFrom64(string encodedData)
	// {
	// 	System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
	// 	System.Text.Decoder utf8Decode = encoder.GetDecoder();
	// 	byte[] todecode_byte = Convert.FromBase64String(encodedData);
	// 	int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
	// 	char[] decoded_char = new char[charCount];
	// 	utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
	// 	string result = new String(decoded_char);
	// 	return result;
	// }
}

using System.Data.SqlTypes;
using Models;
using SQLRepository;

namespace Manager;

public class UserManager
{
	private SQLUserRepository sqlUserRepository = null;

	public UserManager()
	{
		this.sqlUserRepository = new SQLUserRepository();
	}
	public IList<User> GetUsers()
	{
		IList<User> users = new List<User>();

		try
		{
			users = this.sqlUserRepository.GetUsers();
		}
		catch (System.Exception ex)
		{
			throw;
		}

		return users;
	}

	public bool AddUser(User user)
	{
		bool success = false;
		try
		{
			if (!user.IsValid())
			{
				throw new Exception("User is not valid");
			}
			if (this.IsDuplicateUser(user))
			{
				throw new Exception("User same username already exists");
			}
			if(this.IsDuplicateEmail(user))
			{
				throw new Exception("User email is already exists");
			}

			success = this.sqlUserRepository.AddUser(user);

		}
		catch (Exception ex)
		{
			throw;
		}

		return success;
	}

	private bool IsDuplicateUser(User user)
	{
		IEnumerable<User> users = this.GetUsers().Where(x => x.Username.Equals(user.Username));
		int count = users.Where(x => x.Id == user.Id).Count();

		bool result = users.Any(x => x.Id == user.Id);
		return result;
	}
	private bool IsDuplicateEmail(User user)
	{
		IEnumerable<User> users = this.GetUsers().Where(x => x.Email.Equals(user.Email));

		int count = users.Where(x => x.Id != user.Id).Count();
		return count > 0;
	}

	public User IsUserNameExists(string username)
	{
		User user = new User();
		user = this.GetUsers().Where(x=>x.Username.Equals(username)).FirstOrDefault();
		return user;
	}

	public bool IsPasswordMatch( string providedPassword,string hashedPassword)
	{
		bool result;
 		result = this.sqlUserRepository.VerifyPassword(providedPassword, hashedPassword);
		return result;
	}
}

﻿using System.Data.SqlTypes;
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
        if(!user.IsValid())
        {
            throw new Exception("User is not valid");
        }

        success = this.sqlUserRepository.AddUser(user);

        return success;
    }
}

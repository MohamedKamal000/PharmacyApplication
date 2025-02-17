﻿namespace DataAccess
{
    public enum Roles
    {
        User,
        Admin
    }
    
    public interface IUserRole
    { 
        Roles GetUserRole();
        string IdentifyUser();
        
    }

    public class User : IUserRole
    {
        private readonly string userPhone;

        public User(string userIdentifier)
        {
            userPhone = userIdentifier;
        }
        
        public Roles GetUserRole()
        {
            return Roles.User;
        }

        public string IdentifyUser()
        {
            return userPhone;
        }
        
    }

    public class Admin : IUserRole
    {
        private readonly string userPhone;

        public Admin(string userIdentifier)
        {
            userPhone = userIdentifier;
        }
        
        public Roles GetUserRole()
        {
            return Roles.Admin;
        }

        public string IdentifyUser()
        {
            return userPhone;
        }
        
    }
}
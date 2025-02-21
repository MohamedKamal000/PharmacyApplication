using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;



namespace Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            Users user = new Users()
            {
                UserName = "Salah",
                PhoneNumber = "013334",
                Password = "Lol___",
                Role = true
            };

            UsersLogin.Login(user.PhoneNumber, user.Password,out IUserRole use);

            bool r = UsersLogin.ChangePassword(use, "HamadaHelal",user.Password);
            
            Console.WriteLine(r);
        }
    }
}
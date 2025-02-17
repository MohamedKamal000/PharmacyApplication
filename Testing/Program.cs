using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;


namespace Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*PasswordHasher passwordHasher = new PasswordHasher();
            int result = TableManager<Users>.InsertIntoTable(new Dictionary<Users, object>()
            {
                { Users.PhoneNumber , "0122"},
                { Users.Email , "LolForExam"},
                { Users.UserName , "Sayeed"},
                { Users.Password , passwordHasher.Hash("HelloWorld")},
                { Users.Role , false}
            });*/

            try
            {
                throw new Exception("Hi");
            }
            catch (Exception e)
            {
                DB_Logging.LogErrorMessage(e.Message,e.StackTrace);
            }
        }
    }
}
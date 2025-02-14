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
            /*Roles test = TableManager<Users>.Login("Salah", "12345").GetUserRole();
            Console.WriteLine(test.ToString());
            */

            if (TableManager<Users>.SelectFromTable(
                    new KeyValuePair<Users, object>(Users.Email, null), out DataTable dt))
            {
                Console.WriteLine("yes");
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"Name: {(string)dr["UserName"]}");
                    Console.WriteLine($"ID: {(int) dr["ID"]}");
                    Console.WriteLine($"PhoneNumber: {(string)dr["PhoneNumber"]}");
                    Console.WriteLine($"Password: {(string)dr["Password"]}");
                    Console.WriteLine(dr["Email"] == DBNull.Value ? 
                        "Email: is null" : $"Email: {(string)dr["Email"]}");
                    Console.WriteLine($"Role: {(bool) dr["Role"]}");
                }
            }
            
            
        }
    }
}
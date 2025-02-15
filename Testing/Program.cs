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

            /*if (TableManager<Users>.SelectFromTable(
                    new KeyValuePair<Users, object>(Users.Role, false), out DataTable dt))
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
            */


            /*int result = TableManager<Users>.InsertIntoTable(new Dictionary<Users, object>()
            {
                {
                    Users.UserName, "Magdy"
                },
                {
                    Users.Email, null
                },
                {
                    Users.PhoneNumber, "001122"
                },
                {
                    Users.Password, "hamadaAl3ra"
                },
                {
                    Users.Role, true
                }
            });*/
            
            /*
            int result = TableManager<Users>.UpdateTable(new KeyValuePair<Users, object>(Users.Email,null),
                new Dictionary<Users, object>()
                {
                    {Users.PhoneNumber, "0150055"},
                    {Users.UserName,"Mohamed Salah"},
                    {Users.Role , false},
                    {Users.Email, "MohamedSalah@.com"},
                    { Users.Password, "00112233"}
                });
                */
            
            int result = TableManager<Users>.DeleteFromTable(new KeyValuePair<Users, object>(Users.ID, 12));
            Console.WriteLine(result);
        }
    }
}
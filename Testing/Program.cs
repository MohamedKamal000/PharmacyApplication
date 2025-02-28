using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;



namespace Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MedicalProducts user = new MedicalProducts()
            {
               ProductName = "feagra",
               Price = 500,
               Stock = 10,
               ItemDescription = "IDK ?? what do you expect",
               ProductSubCategory = 1,
               ProductCategory = 2
            };
            
            TableManager<MedicalProducts> T = new TableManager<MedicalProducts>(
                new SqlConnection(DBSettings.connectionString),
                new DataBase_Logger());


            int resultID = T.InsertIntoTable(user);
            
            
            Console.WriteLine(T.SelectFromTable(new KeyValuePair<string,object>(
                "ID", resultID
                ),out List<MedicalProducts> u));
            
            Console.WriteLine(u[0].ProductName);


            user.ProductName = "HoneyBag";
            
            int result = T.UpdateTable(new KeyValuePair<string, object>("ID", resultID), user);
            
            Console.WriteLine(result);
            
            /*
            Console.WriteLine(T.DeleteFromTable(new KeyValuePair<string, object>("ProductName","HoneyBag")));
        */
        }
    }
}
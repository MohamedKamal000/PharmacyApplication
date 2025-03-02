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
            
            GenericRepository<MedicalProducts> T = new GenericRepository<MedicalProducts>(
                new SqlConnection(DBSettings.connectionString),
                new DataBase_Logger());


            int resultID = T.Add(user);
            
            
            Console.WriteLine(T.Get(new KeyValuePair<string,object>(
                "ID", resultID
                ),out IEnumerable<MedicalProducts> u));

            List<MedicalProducts> uResult = u.ToList();
            Console.WriteLine(uResult[0].ProductName);


            user.ProductName = "HoneyBag";
            
            int result = T.Update(new KeyValuePair<string, object>("ID", resultID), user);
            
            Console.WriteLine(result);
            
            /*
            Console.WriteLine(T.DeleteFromTable(new KeyValuePair<string, object>("ProductName","HoneyBag")));
        */
        }
    }
}
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DataAccess.Interfaces;


namespace Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DapperContext test = new DapperContext();
            
            Console.WriteLine(test.connectionString);
            Console.ReadKey();
        }

    }
}
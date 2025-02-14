using DataAccess;
using System;


namespace Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Roles test = TableManager<Users>.Login("Salah", "12345").GetUserRole();
            Console.WriteLine(test.ToString());
            
        }
    }
}
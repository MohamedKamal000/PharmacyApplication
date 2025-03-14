using DomainLayer;
using InfrastructureLayer;
using Microsoft.EntityFrameworkCore;


namespace FastManualTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationDbContext t = new ApplicationDbContext())
            {
                var users = t.Users.
                    Include(u => u.Orders)
                    .ThenInclude(o => o.Product)
                    .Include(u => u.Orders)
                    .ThenInclude(o => o.DeliveryMan)
                    .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderStatus)
                    .ToList();

                foreach (var U in users)
                {
                    Console.WriteLine(U.ToString());
                    foreach (var o in U.Orders)
                    {
                        Console.WriteLine("User Orders Below ---");
                        Console.WriteLine(o.ToString());
                    }
                }
            } 


            Console.WriteLine("finished");
        }
    }
}

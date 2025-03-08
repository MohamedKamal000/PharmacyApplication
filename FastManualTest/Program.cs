



using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;
using InfrastructureLayer;
using InfrastructureLayer.Repositories;

namespace FastManualTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IUserRepository<Users> T = new UserRepository(
                new DapperContext()
            );

            Users u = new Users()
            {
                PhoneNumber = "01000000000"
            };

            List<Orders> o = T.GetUserOrders(u).ToList();

            foreach (var oo in o)
            {
                Console.WriteLine(oo.ToString());
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}





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

            UserOder o = T.GetUserOrders(u);

            Console.WriteLine(o.ToString());
            

            //IExtendedRepository<Orders> T = new OrdersRepository(

            //    new DapperContext());

            //List<Orders> TT = T.GetAll().ToList();


            //foreach (var o in TT)
            //{
            //    Console.WriteLine(o.ToString());
            //}
        }
    }
}

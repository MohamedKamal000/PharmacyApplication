



using DataAccess;


namespace FastManualTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DapperContext test = new DapperContext();

            UsersLogin t = new UsersLogin(new DataBaseSystemTrackingLogger(test),
                new UserRepository(test, new DataBaseSystemTrackingLogger(test)),
                new PasswordHasher());

            Console.WriteLine(t.GetUser("string"));
            Console.ReadKey();

        }
    }
}

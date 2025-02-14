namespace DataAccess
{
    public enum Roles
    {
        User,
        Admin
    }
    
    public interface IUserRole
    {
        Roles GetUserRole();
    }

    public class User : IUserRole
    {
        public Roles GetUserRole()
        {
            return Roles.User;
        }
    }

    public class Admin : IUserRole
    {
        public Roles GetUserRole()
        {
            return Roles.Admin;
        }
    }
}
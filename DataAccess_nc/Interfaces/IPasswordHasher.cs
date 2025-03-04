namespace DataAccess
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string enteredPassword,string password);
    }
}
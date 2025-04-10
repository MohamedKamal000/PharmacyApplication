namespace DomainLayer.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string enteredPassword,string password);
    }
}
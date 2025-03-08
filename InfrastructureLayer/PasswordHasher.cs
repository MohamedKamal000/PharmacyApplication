using System.Security.Cryptography;
using DomainLayer.Interfaces;

namespace InfrastructureLayer
{
    public class PasswordHasher : IPasswordHasher
    {
        private  const int HASHSIZE = 32;
        private  const int SALTSIZE = 16;
        private const int ITERATIONS = 100000;

        
        
        public string Hash(string password)
        {

            byte[] salt = new byte[SALTSIZE];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt); 
            }

            byte[] hash = new byte[HASHSIZE];
            using (var pbdkf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS))
            {
                byte[] key = pbdkf2.GetBytes(HASHSIZE);
                using (var sha512 = SHA512.Create())
                {
                    hash = sha512.ComputeHash(key); 
                    
                    return $"{Convert.ToBase64String(hash)}-{Convert.ToBase64String(salt)}";
                }
            }
        }

        public bool Verify(string enteredPassword, string password)
        {
            string[] parts = password.Split('-');
            byte[] hash = Convert.FromBase64String(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            
            using (var pbdkf2 = new Rfc2898DeriveBytes(enteredPassword, salt, ITERATIONS))
            {
                byte[] key = pbdkf2.GetBytes(HASHSIZE);
                using (var sha512 = SHA512.Create())
                {
                    byte[] enteredHash = sha512.ComputeHash(key);

                    return ConstantTimeEquals(enteredHash, hash); 
                }
            }
        }
        
        
        private static bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int result = 0;

            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            
            return result == 0;
        }
    }
}
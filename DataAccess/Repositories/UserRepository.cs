using System;
using System.Data;
using System.Linq;
using Dapper;

namespace DataAccess
{
    public class UserRepository : GenericRepository<Users>, IUserRepository<Users>
    {
        
        public UserRepository(IDbConnection connection, ILogger logger) : base(connection, logger)
        {
            
        }

        public Orders GetUserOrders(Users user)
        {
            // implements the User Query here for his Orders
            
            throw new System.NotImplementedException();
        }
        
        public  Users RetrieveUserCredentials(string phoneNumber)
        {
            Users user = new Users();
            try
            {
                user = _dbConnection.Query<Users>(
                    DBSettings.ProceduresNames.UserLogin.ToString(),
                    new
                    {
                        PhoneNumber = phoneNumber
                    },
                    commandType: CommandType.StoredProcedure
                ).Single();

                if (user.PhoneNumber.Length == 0)
                {
                    user = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Happen: {e}");
                _logger.LogErrorMessage(e.Message,e.StackTrace);
            }
                
            return user;
        }
       
    }
}
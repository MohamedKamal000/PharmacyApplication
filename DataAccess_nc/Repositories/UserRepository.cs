using System;
using System.Data;
using System.Linq;
using Dapper;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class UserRepository : GenericRepository<Users>, IUserRepository<Users>
    {
        
        public UserRepository(IConnection connection, ISystemTrackingLogger systemTrackingLogger) : base(connection, systemTrackingLogger)
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
                ).SingleOrDefault();

            }
            catch (Exception e)
            {
                SystemTrackingLogger.LogErrorMessage(e.Message,e.StackTrace);
                throw;
            }
                
            return user;
        }


    }
}
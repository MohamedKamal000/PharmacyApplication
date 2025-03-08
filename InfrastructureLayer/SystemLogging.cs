using System.Data;
using Dapper;
using DomainLayer.Interfaces;

namespace InfrastructureLayer
{
    public  class DataBaseSystemTrackingLogger : ISystemTrackingLogger
    {

        private readonly IConnection _dbConnection;
        public DataBaseSystemTrackingLogger(IConnection dpConnection)
        {
            _dbConnection = dpConnection;
        }

        public  void LogAdminBehaviour(string adminIdentity, string logMessage)
        {
            string query = @"Insert Into SystemAdminTracking(AdminPhoneIdentifier,AccessDate,BehaviourLog)
                            Values(
                            @AdminIdentifier,
                            @Date,
                            @Behaviour
                            )";

            IDbConnection connection = _dbConnection.CreateConnection();
            

            connection.Execute(
                query,
                    new
                    {
                        AdminIdentifier = adminIdentity,
                        Date = DateTime.Now,
                        Behaviour = logMessage
                    },
                commandType: CommandType.Text
                );
        }

        public  void LogErrorMessage(string errorMessage, string errorStack)
        {
            string query = @"Insert Into SystemErrors(ErrorMessage, ErrorStack,ErrorDate)
                            Values(
                            @ErrorMessage,
                            @ErrorStack,
                            @ErrorDate
                            )";


            IDbConnection connection = _dbConnection.CreateConnection();


            connection.Execute(
                query,
                new
                {
                    ErrorMessage = errorMessage,
                    ErrorDate = DateTime.Now,
                    ErrorStack = errorStack
                },
                commandType: CommandType.Text
            );

        }
        
        
    }
}
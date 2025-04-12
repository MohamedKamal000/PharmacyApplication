using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace InfrastructureLayer.Logging
{
    public class DataBaseLogger : ILogger
    {
        private readonly DataBaseOptions _dataBaseOptions;
        private readonly string _category;
        private Dictionary<string, Action<string,string>> LogBehaviourType;
        
        public DataBaseLogger(DataBaseOptions dataBaseOptions,string category)
        {
            _category = category;
            _dataBaseOptions = dataBaseOptions;
            LogBehaviourType = new Dictionary<string, Action<string, string>>();
            LogBehaviourType.Add("AdminAction",LogAdminBehaviour);
            LogBehaviourType.Add("SystemAction",LogSystemBehaviour);
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (eventId.Name == null) return;
            if (!LogBehaviourType.ContainsKey(eventId.Name)) return; // idk if thats the right way to solve this bug or not
            
            if (!IsEnabled(logLevel))
                return;

            string message = formatter(state, exception);
            string logLevelText = logLevel.ToString();
            
            LogBehaviourType[eventId.Name!](message, logLevelText);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return !(logLevel <= LogLevel.Debug) && logLevel != LogLevel.None;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }


        private void LogSystemBehaviour(string message, string logLevelText)
        {
            
            SqlConnection connection = new SqlConnection(_dataBaseOptions.SqlConnection);

            string query = @"Insert into SystemErrors(ErrorMessage,ErrorDate,Category,LogLevel) 
                             Values(
                                    @message,
                                    @ErrorDate,
                                    @Category,
                                    @LogLevel
                             )";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@message", message);
                command.Parameters.AddWithValue("@ErrorDate",DateTime.Now);
                command.Parameters.AddWithValue("@Category", _category);
                command.Parameters.AddWithValue("@LogLevel", logLevelText);

                using (connection)
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }   
            }
        }

        private void LogAdminBehaviour(string message, string logLevelText)
        {
            
            SqlConnection connection = new SqlConnection(_dataBaseOptions.SqlConnection);

            string query = @"Insert into AdminsBehaviour(ActionMade,TimeStamp,Category,LogLevel)
                             Values(
                                    @message,
                                    @ErrorDate,
                                    @Category,
                                    @LogLevel
                             )";


            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@message", message);
                command.Parameters.AddWithValue("@ErrorDate",DateTime.Now);
                command.Parameters.AddWithValue("@Category", _category);
                command.Parameters.AddWithValue("@LogLevel", logLevelText);
                using (connection)
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }   
            }
        }
    }
    
}
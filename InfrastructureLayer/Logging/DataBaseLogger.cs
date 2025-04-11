using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace InfrastructureLayer.Logging
{
    public class DataBaseLogger : ILogger
    {
        private readonly DataBaseOptions _dataBaseOptions;
        private readonly string _category;
        private LogLevel _currentAcceptedLogLevel;
        private Dictionary<string, Action<string,string>> LogBehaviourType;
        
        public DataBaseLogger(DataBaseOptions dataBaseOptions,string category)
        {
            _category = category;
            _dataBaseOptions = dataBaseOptions;
            _currentAcceptedLogLevel = LogLevel.Error;
            LogBehaviourType = new Dictionary<string, Action<string, string>>();
            LogBehaviourType.Add("AdminAction",LogAdminBehaviour);
            LogBehaviourType.Add("SystemAction",LogSystemBehaviour);
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            _currentAcceptedLogLevel = ParseLogLevels(ParseLoggerEventName(eventId.Name!));
            
            if (!IsEnabled(logLevel))
                return;

            string message = formatter(state, exception);
            string logLevelText = logLevel.ToString();
            
            LogBehaviourType[eventId.Name!](message, logLevelText);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _currentAcceptedLogLevel <= logLevel;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }


        private void LogSystemBehaviour(string message, string logLevelText)
        {
            
            SqlConnection connection = new SqlConnection(_dataBaseOptions.SqlConnection);

            string query = @"Insert into SystemErrors 
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

            string query = @"Insert into AdmisBehaviour 
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

        private LogLevel ParseLogLevels(string logLevelString)
        {
            switch (logLevelString?.ToLowerInvariant())
            {
                case "trace":
                    return LogLevel.Trace;
                case "debug":
                    return LogLevel.Debug;
                case "information":
                case "info":
                    return LogLevel.Information;
                case "warning":
                case "warn":
                    return LogLevel.Warning;
                case "error":
                    return LogLevel.Error;
                case "critical":
                    return LogLevel.Critical;
                case "none":
                    return LogLevel.None;
                default:
                    throw new ArgumentException($"Invalid log level: {logLevelString}");
            }
        }

        private string ParseLoggerEventName(string eventName)
        {
            if (eventName != "AdminAction" && eventName != "SystemAction")
            {
                return "none";
            }
            return (eventName == "AdminAction" ? 
                _dataBaseOptions.AdminBehaviour : _dataBaseOptions.SystemBehaviour);
        }
    }
    
}
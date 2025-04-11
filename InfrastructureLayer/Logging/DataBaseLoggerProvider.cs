using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer.Logging
{
 
    public class DataBaseLoggerProvider : ILoggerProvider
    {

        private readonly DataBaseOptions _loggingOptions;
        
        public DataBaseLoggerProvider(DataBaseOptions loggingOptions)
        {
            _loggingOptions = loggingOptions;
            
        }
        
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DataBaseLogger(_loggingOptions,categoryName);
        }
    }    
    
}

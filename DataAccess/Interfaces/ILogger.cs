namespace DataAccess
{
    public interface ILogger
    {
        void LogAdminBehaviour(string adminIdentity,string logMessage);
        
        void LogErrorMessage(string errorMessage, string errorStack);
    }
}
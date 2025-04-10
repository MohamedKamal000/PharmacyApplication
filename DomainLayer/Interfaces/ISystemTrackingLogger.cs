namespace DomainLayer.Interfaces
{
    public interface ISystemTrackingLogger
    {
        void LogAdminBehaviour(string adminIdentity,string logMessage);
        
        void LogErrorMessage(string errorMessage, string errorStack);
    }
}
namespace DataAccess
{
    public static class DBSettings
    {
       // public static string connectionString = "Server=.;Database=PharmacyApplication;User Id=sa;Password=sa123456;";


        public enum ProceduresNames
        {
            UserLogin,
            InsertIntoAnyTable,
            SelectFromTable,
            UpdateTable,
            DeleteFromTable,
            CheckRowInTable
        }
    }
}
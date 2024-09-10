namespace CallCenterAgentManager.CrossCutting.Settings
{
    public class Settings
    {

        #region DataBase
        public static string? RelationalDbConnectionString { get; set; }
        public static string? NoSqlDbConnectionString { get; set; }
        public static string? NoSqlDatabaseName { get; set; }
        public static bool UseNoSqlDatabase { get; set; }
        #endregion

        #region Swagger
        public static string? SwaggerAPIName;
        public static string? SwaggerAPIVersion;
        #endregion
    }
}
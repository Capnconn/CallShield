namespace CallShield.Common.Configuration
{
    public static class DatabaseConfiguration
    {
        public static string DatabasePath
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CallShield", "callshield.db");
    }
}

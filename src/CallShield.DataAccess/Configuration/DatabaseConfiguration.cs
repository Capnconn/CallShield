using Microsoft.Maui.Storage;
using SQLite;

namespace CallShield.DataAccess.Configuration
{
    public static class DatabaseConfiguration
    {
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
            => Path.Combine(FileSystem.AppDataDirectory, "callshield.db");
    }
}

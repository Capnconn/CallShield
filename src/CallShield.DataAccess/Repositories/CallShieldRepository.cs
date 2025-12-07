using CallShield.DataAccess.Configuration;
using CallShield.DataAccess.Models;
using SQLite;

namespace CallShield.DataAccess.Repositories
{
    public interface ICallShieldRepository
    {
        void BuildDatabase();

        List<BlockedCall> GetAllBlockedCalls();

        void InsertBlockedCall(BlockedCall callDetails);
    }

    public class CallShieldRepository : ICallShieldRepository
    {
        private readonly SQLiteConnection _dbConnection = new(DatabaseConfiguration.DatabasePath, DatabaseConfiguration.Flags);

        public void BuildDatabase()
        {
            _dbConnection.CreateTable<BlockedCall>();

#if DEBUG
            _dbConnection.DeleteAll<BlockedCall>();

            if (_dbConnection.Query<BlockedCall>("SELECT COUNT(*) FROM blocked_call").Count < 50)
            {
                for (int i = 0; i < 50; i++)
                {
                    this.InsertBlockedCall(new BlockedCall
                    {
                        Name = $"Test Name {i}",
                        PhoneNumber = $"555-000-{i:D4}",
                        Date = DateTime.Now.AddHours(i * -5)
                    });
                }
            }
#endif
        }

        public List<BlockedCall> GetAllBlockedCalls()
            => _dbConnection.Query<BlockedCall>("SELECT * FROM blocked_call ORDER BY date");

        public void InsertBlockedCall(BlockedCall callDetails)
        {
            _dbConnection.Insert(new BlockedCall
            {
                PhoneNumber = callDetails.PhoneNumber,
                Name = callDetails.Name,
                Date = callDetails.Date
            });
        }
    }
}

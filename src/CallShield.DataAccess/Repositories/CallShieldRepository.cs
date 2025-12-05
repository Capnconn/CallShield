using CallShield.Common.Configuration;
using CallShield.DataAccess.Models;
using SQLite;

namespace CallShield.DataAccess.Repositories
{
    public interface ICallShieldRepository
    {
        void BuildDatabase();

        List<Common.Models.BlockedCall> GetAllBlockedCalls();

        void InsertBlockedCall(CallDetails callDetails);

        void TryInsertCallDetails(Common.Models.CallDetails callDetails);
    }

    public class CallShieldRepository : ICallShieldRepository
    {
        private readonly SQLiteConnection _dbConnection = new SQLiteConnection(DatabaseConfiguration.DatabasePath);

        public void BuildDatabase()
        {
            if (!File.Exists(_dbConnection.DatabasePath))
            {
                _dbConnection.CreateTable<CallDetails>();
                _dbConnection.CreateTable<BlockedCall>();

                _dbConnection.Execute("ALTER TABLE call_details " +
                                      "ADD FOREIGN KEY (phone_number) REFERENCES blocked_call(phone_number);");

            }
        }

        public List<Common.Models.BlockedCall> GetAllBlockedCalls()
        {
            var listOfBlockedCalls = _dbConnection.Query<BlockedCall>("SELECT * FROM blocked_calls ORDER BY date");

            var convertedBlockedCalls = new List<Common.Models.BlockedCall>();

            listOfBlockedCalls.ForEach(x =>
            {
                convertedBlockedCalls.Add(new Common.Models.BlockedCall(x.Date));
            });

            return convertedBlockedCalls;
        }

        public void InsertBlockedCall(CallDetails callDetails)
        {
            if (_dbConnection.Query<CallDetails>("SELECT * FROM call_details WHERE phone_number = ?", callDetails.PhoneNumber).FirstOrDefault() is null)
            {
                _dbConnection.Insert(callDetails);
            }

            _dbConnection.Insert(new BlockedCall
            {
                PhoneNumber = callDetails.PhoneNumber,
                Date = DateTime.Now
            });
        }

        public void TryInsertCallDetails(Common.Models.CallDetails callDetails)
        {
            if (!_dbConnection.Query<CallDetails>("SELECT COUNT(*) FROM call_details").Any())
            {
                _dbConnection.Insert(new CallDetails
                {
                    Date = callDetails.Date,
                    PhoneNumber = callDetails.PhoneNumber,
                    Name = callDetails.Name
                });
            }

            _dbConnection.Insert(new BlockedCall
            {
                Date = callDetails.Date,
                PhoneNumber = callDetails.PhoneNumber
            });
        }
    }
}

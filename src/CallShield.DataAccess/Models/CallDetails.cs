using SQLite;

namespace CallShield.DataAccess.Models
{
    [Table("CallDetails")]
    public class CallDetails(string name, string phoneNumber, DateTime date)
    {
        [PrimaryKey]
        [Column("date")]
        public string Date { get; } = date.ToString("MMMM dd, yyyy");

        [PrimaryKey]
        [Column("phone_number")]
        public string PhoneNumber { get; } = phoneNumber;

        [Column("name")]
        public string Name { get; } = name;

        [Column("Time")]
        public string Time { get; } = string.Format("{0:t}", date);
    }
}

using SQLite;

namespace CallShield.DataAccess.Models
{
    [Table("blocked_call")]
    public class BlockedCall()
    {
        [PrimaryKey]
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}

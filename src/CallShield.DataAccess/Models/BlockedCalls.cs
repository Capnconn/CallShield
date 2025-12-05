using SQLite;

namespace CallShield.DataAccess.Models
{
    [Table("blocked_call")]
    public class BlockedCall
    {
        [PrimaryKey]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("date")]
        public DateTime Date { get; set; }    
    }
}

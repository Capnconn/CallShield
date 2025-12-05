using SQLite;

namespace CallShield.DataAccess.Models
{
    [Table("call_details")]
    public class CallDetails()
    {
        [Column("date")]
        public DateTime Date { get; set; }

        [PrimaryKey]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(BlockedCall))]
        public List<BlockedCall> BlockedCalls { get; } = [];
    }
}

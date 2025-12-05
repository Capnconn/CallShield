namespace CallShield.Common.Models
{
    public class BlockedCall(DateTime date)
    {
        public DateTime Date { get; set; } = date;
    }
}

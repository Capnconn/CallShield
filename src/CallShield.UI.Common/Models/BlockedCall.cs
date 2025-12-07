namespace CallShield.UI.Common.Models
{
    public class BlockedCall(DateTime date, List<CallDetails> callDetails) : List<CallDetails>(callDetails)
    {
        public DateTime Date { get; set; } = date;
    }
}

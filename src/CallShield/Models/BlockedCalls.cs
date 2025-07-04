using CallShield.DataAccess.Models;

namespace CallShield.UI.Models
{
    public class BlockedCalls(string date, List<CallDetails> callDetails) : List<CallDetails>(callDetails)
    {
        public string Date { get; set; } = date;
    }
}

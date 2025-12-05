namespace CallShield.Common.Models
{
    public class CallDetails(string name, string phoneNumber, DateTime date)
    {
        public DateTime Date { get; } = date;
        public string Name { get; } = name;
        public string PhoneNumber { get; } = phoneNumber;
        public string Time { get; } = string.Format("{0:t}", date);
    }
}

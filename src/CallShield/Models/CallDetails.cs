namespace CallShield.DataAccess.Models
{
    public class CallDetails(string name, string phoneNumber, DateTime date)
    {
        public string Date { get; } = date.ToString("MMMM dd, yyyy");
        public string Name { get; } = name;
        public string PhoneNumber { get; } = phoneNumber;
        public string Time { get; } = string.Format("{0:t}", date);
    }
}

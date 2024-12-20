namespace TSR_Backend.Models
{
    public class UserModelLogin
    {
        public string? Role { get; set; }
        public string? DynamicToken { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string? Username { get; set; }
        public int Uid { get; set; }
        public bool IsCustomer { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }
    }
}

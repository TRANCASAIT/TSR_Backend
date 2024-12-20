namespace TSR_Backend.Models
{
    public class CustomerUser
    {
        public string Username { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int UserTypeId { get; set; }
        public int CustomerId { get; set; }

        public class CustomerUserUpdate: CustomerUser
        {
            public int CustomerUserId { get; set; }
        }

        public class CustomerUserGet: CustomerUserUpdate
        {
            public string UserTypeName { get; set; }
            public string CustomerName { get; set; }
            public bool Status { get; set; }
            public int State { get; set; }
            public string Message { get; set; }
        }

        public class CustomerUserUpdateStatus
        {
            public bool Status { get; set; }
            public int CustomerUserId { get; set; }
        }
    }
}

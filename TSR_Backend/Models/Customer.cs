namespace TSR_Backend.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string RFC { get; set; }
        public string Street { get; set; }
        public string ExtNumber { get; set; }
        public string IntNumber { get; set; }
        public string ZipCode { get; set; }
        public string Suburb { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }

        public class CustomerUpdate: Customer
        {
            public int CustomerId { get; set; }
        }

        public class CustomerGet: CustomerUpdate
        {
            public string CityName { get; set; }
            public string StateName { get; set; }
            public bool Status { get; set; }
            public string Address { get; set; }
            public int State { get; set; }
            public string? Message { get; set; }
        }

        public class CustomerStatus
        {
            public int CustomerId { get; set; }
            public bool Status { get; set; }
        }
    }
}

namespace TSR_Backend.Models
{
    public class UserType
    {
        public string UserTypeName { get; set; }

        public class UserTypeUpdate: UserType
        {
            public int UserTypeId { get; set; }
        }
        public class UserTypeGet: UserTypeUpdate
        {
            public int State { get; set; }
            public string? Message { get; set; }
        }
    }
}

namespace TSR_Backend.Models
{
    public class Status
    {
        public string StatusName { get; set; }
        public class StatusUpdate : Status
        {
            public int StatusId { get; set; }
        }
        public class StatusGet: StatusUpdate
        {
            public int State { get; set; }
            public string? Message { get; set; }
        }
    }
}

namespace TSR_Backend.Models
{
    public class DecompleteReason
    {
        public int ReasonId { get; set; }
        public string Reason { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }
    }

    public  class DecompletedRequests
    {
        public int ServiceRequestId { get; set; }
        public string Status { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }
    }

    public class DecompletedRequestsFullContext
    {
        public int ServiceRequestId { get; set; }
        public string Reason { get; set; }
        public string LastModifiedBy { get; set; }
        public string ModifiedAt { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }
    }
}

namespace TSR_Backend.Models
{
    public class ServiceRequestReport
    {
        public int ServiceRequestId { get; set; }
        public int DocumentId { get; set; }
        public string CustomerName { get; set; }
        public string BoxNumber { get; set; }
        public string Uuid { get; set; }
        public string OperationTypeName { get; set; }
        public string Reference { get; set; }
        public int StopNumber { get; set; }
        public string CreatedAt { get; set; }
        public string TmwOrder { get; set; }
        public string StatusDescription { get; set; }
        public int StatusId { get; set; }
        public string Inward { get; set; }
        public string Ace { get; set; }
        public string Layout { get; set; }
        public string AcceptedBy { get; set; }
        public string LayoutAccepteddtm { get; set; }
        public string ConsignmentNote { get; set; }
        public string Xml { get; set; }
        public string OriginalPdf { get; set; }
        public string OperationsPdf { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }
    }

    public class SearchReport
    {
        public int? StatusId { get; set; }
        public int? OperationTypeId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? BoxNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
    }
}

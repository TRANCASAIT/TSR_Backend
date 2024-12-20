using static TSR_Backend.Models.Document;

namespace TSR_Backend.Models
{
    public class ServiceRequest
    {
        public string BoxNumber { get; set; }
        public string Reference { get; set; }
        public int  CustomerId { get; set; }
        public int  OperationTypeId { get; set; }
        public int  StopsId { get; set; }
        public class ServiceUpdate: ServiceRequest
        {
            public int ServiceRequestId { get; set; }
        }
        public class ServiceGet: ServiceUpdate
        {
            public string CreatedAt { get; set; }
            public bool Priority { get; set; }
            public string CustomerName { get; set; }
            public string InvoiceNumber { get; set; }
            public string StopNumber { get; set; }
            public string OperationTypeName { get; set; }
            public string TmwOrder { get; set; }
            public string Uuid { get; set; }
            public int StatusId { get; set; }
            public string StatusDescription { get; set; }
            public bool InvMxStatus { get; set; }
            public bool InvUSAStatus { get; set; }
            public bool BolStatus { get; set; }
            public bool InwardStatus { get; set; }
            public bool AceStatus { get; set; }
            public bool LayoutStatus { get; set; }
            public bool XmlStatus { get; set; }
            public bool PdfOriginalStatus { get; set; }
            public bool PdfOperationsStatus { get; set; }
            public int State { get; set; }
            public string? Message { get; set; }
            public string TmwTime { get; set; }
            public string CcpTime { get; set; }
        }
        public class RemoveService
        {
            public int ServiceRequestId { get; set; }

        }

        public class UpdateBox: RemoveService
        {
            public string BoxNumber { get; set; }
        }

        public class UpdateReference: RemoveService
        {
            public string Reference { get; set;}
        }

        public class UpdateUuid : RemoveService
        {
            public string Uuid { get; set; }
        }

        public class UpdateOperation: RemoveService
        {
            public int OperationTypeId { get; set; }
        }

        public class UpdatePriority: RemoveService
        {
            public bool Priority { get; set; }
        }

        public class UpdateTmw: RemoveService
        {
            public string TmwOrder { get; set; }
        }

        public class Search
        {
            public int? StatusId { get; set; }
            public int? OperationTypeId { get; set; }
            public string? InvoiceNumber { get; set; }
            public string? BoxNumber { get; set; }
            public int? CustomerId { get; set; }
            public string? Start { get; set; }
            public string? End { get; set; }
            public bool? Priority { get; set; }
        }

        public class Request
        {
            public int CustomerId { get; set; }
            public string BoxNumber { get; set; }
            public string Reference { get; set; }
            public int StopId { get; set; }
            public int OperationTypeId { get; set; }
        }

        public class ChangeRequestStatus
        {
            public int? ServiceRequestId { get; set; }
            public int ReasonId { get; set; }
            public bool OtherReason { get; set; }
            public string Reason { get; set; }
        }
    }
}

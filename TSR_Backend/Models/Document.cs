namespace TSR_Backend.Models
{
    public class Document
    {
        public int ServiceRequestId { get; set; }
        public int DocumentId { get; set; }
        public int StopId { get; set; }
        public int StopNumber { get; set; }
        public int StatusId { get; set; }
        public string StatusDescription { get; set; }
        public bool InvoiceMXStatus { get; set; }
        public string InvoiceMX { get; set; }
        public string InvMXFN { get; set; }
        public string InvMXdtm { get; set; }
        public bool InvoiceMXCompleted { get; set; }
        public bool InvoiceUSAStatus { get; set; }
        public string InvoiceUSA { get; set; }
        public string InvUSAFN { get; set; }
        public string InvUSAdtm { get; set; }
        public bool InvoiceUSACompleted { get; set; }
        public bool BOLStatus { get; set; }
        public string BOL { get; set; }
        public string BolFN { get; set; }
        public string Boldtm { get; set; }
        public bool BolCompleted { get; set; }
        public bool InwardStatus { get; set; }
        public string Inward { get; set; }
        public string InwFN { get; set; }
        public string Inwdtm { get; set; }
        public bool InwardCompleted { get; set; }
        public bool ACEStatus { get; set; }
        public string ACE { get; set; }
        public string AceFN { get; set; }
        public string Acedtm { get; set; }
        public bool AceCompleted { get; set; }
        public bool LayoutStatus { get; set; }
        public string Layout { get; set; }
        public string LayoutFN { get; set; }
        public string Layoutdtm { get; set; }
        public bool LayoutCompleted { get; set; }
        public bool Accepted_Layout { get; set; }
        public string ConsignmentNote { get; set; }
        public bool XmlStatus { get; set; }
        public string XML { get; set; }
        public string XmlFN { get; set; }
        public string Xmldtm { get; set; }
        public bool XmlCompleted { get; set; }
        public bool OriginPdfStatus { get; set; }
        public string OriginalPDF { get; set; }
        public string OPdfFN { get; set; }
        public string OPdfdtm { get; set; }
        public bool OriginalPDFCompleted { get; set; }
        public bool OPStatus { get; set; }
        public string OperationsPDF { get; set; }
        public string Reference { get; set; }
        public string OpPdfFN { get; set; }
        public string OpPdfdtm { get; set; }
        public bool OperationsPDFCompleted { get; set; }
        public bool AcceptedLayoutDC { get; set; }
        public bool NotAcceptedLayoutDC { get; set; }
        public int State { get; set; }
        public string? Message { get; set; }

        public class DocumentIdentifier
        {
            public int DocumentId { get; set; }
        }

        public class UpdateConsignmentNote : DocumentIdentifier
        {
            public string ConsignmentNote { get; set; }
        }

        

        public class UploadFile : DocumentIdentifier
        {
            public int ServiceRequestId { get; set; }
            public int StopNumber { get; set; }
            public int DocumentType { get; set; }
            public IFormFile DocumentFile { get; set; }
        }

        public class DownloadFile
        {
            public string Url { get; set; }
        }

        public class RemoveFile : DocumentIdentifier
        {
            public int ServiceRequestId { get; set; }
            public int DocumentType { get; set; }
            public string Url { get; set; }
            public string FileName { get; set; }
        }

        public class UpdateLayoutStatus: DocumentIdentifier
        {
            public int ServiceRequestId { get; set; }
            public bool AcceptedLayout { get; set; }
            public bool NotAcceptedLayout { get; set; }
        }
    }
}

namespace TSR_Backend.Models
{
    public class OperationType
    {
        public string OperationTypeName { get; set; }
        
        public class OperationTypeUpdate: OperationType
        {
            public int OperationTypeId { get; set; }
        }
        public class OperationTypeGet: OperationTypeUpdate
        {
            public int State { get; set; }
            public string? Message { get; set; }
        }
    }
}

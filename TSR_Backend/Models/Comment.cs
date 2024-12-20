namespace TSR_Backend.Models
{
    public class Comment
    {
        public string CommentBody { get; set; }
        public int DocumentId { get; set; }
        public int ServiceRequestId { get; set; }

        public class CommentGet : Comment
        {
            public int CommentId { get; set; }
            public int UserId { get; set; }
            public bool IsCustomer { get; set; }
            public string Username { get; set; }
            public string CreatedAt { get; set; }
            public int State { get; set; }
            public string? Message { get; set; }
        }
    }
}

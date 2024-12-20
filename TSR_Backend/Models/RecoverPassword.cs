namespace TSR_Backend.Models
{
    public class RecoverPassword
    {
        public string? Email { get; set; }
        public string? Code { get; set; }

        public class ResetPassword : RecoverPassword
        {
            public string? Password { get; set; }
        }
    }
}

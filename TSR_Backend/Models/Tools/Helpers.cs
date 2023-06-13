using System.Security.Cryptography;
using System.Text;
using TSR_Backend.Models.DAO;

namespace TSR_Backend.Models.Tools
{
    public class Helpers
    {
        public static Result BuildResult(int state, string message, bool flag)
        {
            Result result = new Result();
            result.State = state;
            result.Message = message;
            if (flag == true)
            {
                result.RecordsNumber = 0;
            }
            return result;
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null!;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}

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
    }
}

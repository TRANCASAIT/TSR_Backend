using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSR_Backend.Models;
using TSR_Backend.Models.DAO;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetSessionsController : ControllerBase
    {
        [HttpPost("CheckEmailSession/{email}")]
        public IActionResult CheckEmailSession(string email)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.CheckEmailSession(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CheckCodeSession")]
        public IActionResult CheckCodeSession(ResetSession _obj)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.CheckCodeSession(_obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

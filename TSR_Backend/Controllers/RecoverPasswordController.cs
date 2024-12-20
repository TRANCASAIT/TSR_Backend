using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TSR_Backend.Models.RecoverPassword;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoverPasswordController : ControllerBase
    {
        [HttpPost("CheckEmail/{email}")]
        public IActionResult CheckEmail(string email)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.CheckEmail(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CheckCode")]
        public IActionResult CheckCode(RecoverPassword _obj)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.CheckCode(_obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword _obj)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.Reset_Password(_obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

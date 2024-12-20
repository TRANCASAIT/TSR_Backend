using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TSR_Backend.Models.City;
using System.Security.Claims;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using TSR_Backend.Models;
using static TSR_Backend.Models.State;
using System.Data;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        [HttpGet("GetStates")]
        [Authorize]
        public IActionResult GetStates()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<StateGet> resDAO = TSR_DAO.GetStates(currentUser.Uid, currentUser.DynamicToken!);
                if (resDAO[0].State == 1)
                {
                    result = Helpers.BuildResult(resDAO[0].State, resDAO[0].Message!, true);
                    return BadRequest(result);
                }
                else if (resDAO[0].State == 401)
                {
                    result = Helpers.BuildResult(resDAO[0].State, resDAO[0].Message!, true);
                    return BadRequest(result);
                }
                else
                {
                    return Ok(resDAO);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpPost("CreateState")]
        [Authorize]
        public IActionResult CreateState(State _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.CreateState(_obj, currentUser.Uid, currentUser.DynamicToken!);
                if (result.State == 1)
                {
                    return BadRequest(result);
                }
                else if (result.State == 401)
                {
                    return BadRequest(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateState")]
        [Authorize]
        public IActionResult UpdateState(StateUpdate _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateState(_obj, currentUser.Uid, currentUser.DynamicToken!);
                if (result.State == 1)
                {
                    return BadRequest(result);
                }
                else if (result.State == 401)
                {
                    return BadRequest(result);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        private UserModelLogin GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModelLogin
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == "Username")?.Value,
                    CustomerName = userClaims.FirstOrDefault(o => o.Type == "UserEmail")?.Value,
                    DynamicToken = userClaims.FirstOrDefault(o => o.Type == "DynamicToken")?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == "UserGivenName")?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == "UserSurname")?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == "UserRole")?.Value,
                    Uid = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == "Uid")?.Value),
                };
            }
            return null!;
        }
    }
}

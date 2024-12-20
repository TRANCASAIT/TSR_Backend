using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TSR_Backend.Models.CustomerUser;
using System.Security.Claims;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using TSR_Backend.Models;
using static TSR_Backend.Models.User;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<UserGet> res = TSR_DAO.GetUsers(currentUser.Uid, currentUser.DynamicToken!);
                if (res[0].State == 1)
                {
                    result = Helpers.BuildResult(res[0].State, res[0].Message!, true);
                    return BadRequest(result);
                }
                else if (res[0].State == 401)
                {
                    result = Helpers.BuildResult(res[0].State, res[0].Message!, true);
                    return BadRequest(result);
                }
                else
                {
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.CreateUser(obj, currentUser.Uid, currentUser.DynamicToken!);
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
                throw;
            }
        }

        [HttpPost("UpdateUser")]
        [Authorize]
        public IActionResult UpdateUser(UserUpdate _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateUser(_obj, currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("UpdateUserStatus")]
        [Authorize]
        public IActionResult UpdateUserStatus(UserUpdateStatus _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateUserStatus(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    StartDate = userClaims.FirstOrDefault(o => o.Type == "StartDate")?.Value,
                    EndDate = userClaims.FirstOrDefault(o => o.Type == "EndDate")?.Value,
                    Uid = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == "Uid")?.Value),
                };
            }
            return null!;
        }
    }
}

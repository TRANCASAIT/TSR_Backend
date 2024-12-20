using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TSR_Backend.Models;
using static TSR_Backend.Models.City;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.UserType;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {

        [HttpGet("GetUserTypes")]
        [Authorize]
        public IActionResult GetUserTypes()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<UserTypeGet> resDAO = TSR_DAO.GetUserTypes(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpGet("GetUserTypesDrops")]
        [Authorize]
        public IActionResult GetUserTypesDrops()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<UserTypeGet> resDAO = TSR_DAO.GetUserTypesDrops(currentUser.Uid, currentUser.DynamicToken!);
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

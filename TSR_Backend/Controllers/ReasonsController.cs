using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static TSR_Backend.Models.City;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using System.Security.Claims;
using TSR_Backend.Models;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonsController : ControllerBase
    {
        [HttpGet("GetReasons")]
        [Authorize]
        public IActionResult GetReasons()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<DecompleteReason> resDAO = TSR_DAO.GetReasons(currentUser.Uid, currentUser.DynamicToken!);
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


        [HttpGet("GetDecompletedRequests")]
        [Authorize]
        public IActionResult GetDecompletedRequests()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<DecompletedRequests> resDAO = TSR_DAO.GetDecompletedRequests(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpGet("GetDecompletedRequestsFullContext")]
        [Authorize]
        public IActionResult GetDecompletedRequestsFullContext(int service)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<DecompletedRequestsFullContext> resDAO = TSR_DAO.GetDecompletedRequestsFullContext(currentUser.Uid, currentUser.DynamicToken!, service);
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
                    DynamicToken = userClaims.FirstOrDefault(o => o.Type == "DynamicToken")?.Value,
                    CustomerName = userClaims.FirstOrDefault(o => o.Type == "UserEmail")?.Value,
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

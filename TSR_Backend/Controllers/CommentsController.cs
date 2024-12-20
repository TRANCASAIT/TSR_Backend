using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TSR_Backend.Hubs;
using TSR_Backend.Models;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.Comment;
using static TSR_Backend.Models.Document;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public CommentsController( IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("GetComments")]
        [Authorize] 
        public IActionResult GetComments(int id)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<CommentGet> res = TSR_DAO.GetComments(currentUser.Uid,id, currentUser.DynamicToken!);
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

        [HttpPost("SaveComment")]
        [Authorize]
        public async Task<IActionResult> SaveComment(Comment _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.SaveComment(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("MessageAdded");
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

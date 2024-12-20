using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TSR_Backend.Models;
using static TSR_Backend.Models.City;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.ServiceRequest;
using Microsoft.AspNetCore.SignalR;
using TSR_Backend.Hubs;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public ServiceRequestsController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        #region admin-actions
        [HttpGet("GetServices")]
        [Authorize]
        public IActionResult GetServices()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<ServiceGet> resDAO = TSR_DAO.GetServices(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("GetServicesFiltered")]
        [Authorize]
        public IActionResult GetServicesFiltered(Search obj)
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<ServiceGet> resDAO = TSR_DAO.GetServicesFiltered(obj, currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("UpdatePriority")]
        [Authorize]
        public IActionResult UpdatePriority(UpdatePriority _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdatePriority(_obj, currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("ReturnToFileUpload")]
        [Authorize]
        public async Task<IActionResult> ReturnToFileUpload(ChangeRequestStatus _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.ReturnToFileUpload(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateTmw")]
        [Authorize]
        public async Task<IActionResult> UpdateTmw(UpdateTmw _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateTmw(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateUuid")]
        [Authorize]
        public async Task<IActionResult> UpdateUuid(UpdateUuid _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateUuid(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion

        #region customer-actions
        [HttpPost("RemoveServiceRequest")]
        [Authorize]
        public async Task<IActionResult> RemoveService(RemoveService _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.RemoveService(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordRemoved");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateBox")]
        [Authorize]
        public async Task<IActionResult> UpdateBox(UpdateBox _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateBox(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateReference")]
        [Authorize]
        public async Task<IActionResult> UpdateReference(UpdateReference _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateReference(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdateOperation")]
        [Authorize]
        public async Task<IActionResult> UpdateOperation(UpdateOperation _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateOperation(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("RecordUpdated");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreateRequest")]
        [Authorize]
        public async Task<IActionResult> CreateRequest(Request _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.CreateRequest(_obj, currentUser.Uid, currentUser.DynamicToken!);
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
                    await _hubContext.Clients.All.SendAsync("NewRecordAdded");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetServicesCount")]
        [Authorize]
        public IActionResult GetServicesCount()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<Chart> resDAO = TSR_DAO.GetServicesCount(currentUser.Uid, currentUser.DynamicToken!);
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


        #endregion
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

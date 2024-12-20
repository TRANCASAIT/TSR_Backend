using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TSR_Backend.Models;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.City;
using static TSR_Backend.Models.Customer;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<CustomerGet> res = TSR_DAO.GetCustomers(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpGet("GetCustomerForCustomers")]
        public IActionResult GetCustomerForCustomers()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<CustomerGet> res = TSR_DAO.GetCustomerForCustomers(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpGet("GetCustomerForAdm")]
        public IActionResult GetCustomerForAdm()
        {
            Result result = new Result();
            try
            {
                var currentUser = GetCurrentUser();
                List<CustomerGet> res = TSR_DAO.GetCustomerForAdm(currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.CreateCustomer(obj, currentUser.Uid, currentUser.DynamicToken!);
                if(result.State == 1)
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

        [HttpPost("UpdateCustomer")]
        [Authorize]
        public IActionResult UpdateCustomer(CustomerUpdate _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateCustomer(_obj, currentUser.Uid, currentUser.DynamicToken!);
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

        [HttpPost("UpdateCustomerStatus")]
        [Authorize]
        public IActionResult UpdateCustomerStatus(CustomerStatus _obj)
        {
            try
            {
                Result result = new Result();
                var currentUser = GetCurrentUser();
                result = TSR_DAO.UpdateCustomerStatus(_obj, currentUser.Uid, currentUser.DynamicToken!);
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

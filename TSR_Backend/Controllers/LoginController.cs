using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TSR_Backend.Models.DAO;
using TSR_Backend.Models.Tools;
using TSR_Backend.Models;
using System.Security.Cryptography;

namespace TSR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            Result result = new Result();
            try
            {
                var user = Authenticate(userLogin);
                if (user.State == 1)
                {
                    result = Helpers.BuildResult(user.State, user.Message!, false);
                    return BadRequest(result);
                }
                else
                {
                    Token token = new Token();
                    var _token = Generate(user);
                    token.Key = "Token";
                    token.Value = _token;
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("LogOut/{uid}")]
        public IActionResult Logout(int uid)
        {
            Result result = new Result();
            try
            {
                result = TSR_DAO.LogOut(uid);
                if (result.State == 1)
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
                result.State = 1;
                result.Message = ex.Message;
                return BadRequest(result);
            }
        }

        private UserModelLogin Authenticate(LoginUser user)
        {
            var currentUser = TSR_DAO.AuthenticateUser(user);
            return currentUser;
        }

        private string Generate(UserModelLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //create the claims
            var claims = new[]
            {
                new Claim("CustomerName", user.CustomerName!),
                new Claim("UserGivenName", user.FirstName!),
                new Claim("UserSurname", user.LastName!),
                new Claim("UserRole", user.Role!),
                new Claim("Username", user.Username!),
                new Claim("DynamicToken", user.DynamicToken!),
                new Claim("StartDate", user.StartDate!),
                new Claim("EndDate", user.EndDate!),
                new Claim("IsCustomer", String.Concat(user.IsCustomer!)),
                new Claim("Uid", String.Concat(user.Uid)),
                new Claim("CustomerId", String.Concat(user.CustomerId))
            };

            //create token
            var token = new JwtSecurityToken(

                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

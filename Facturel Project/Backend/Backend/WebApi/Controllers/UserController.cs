using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataTransferObjects;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RepositoryLayer;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly IConfiguration configuration;
        #endregion

        #region Constructors
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region Controllers
        [HttpPost("Login")]
        public ActionResult<UserWithTokenDto> Login([FromBody] CredentialsDto credentials)
        {
            using (UnitOfWork UoW = new UnitOfWork())
            {
                var dto = UoW.Users.Login(credentials);
                if (dto is not null)
                {
                    dto.Token = CreateToken(dto.UserId.Value);
                    return Ok(dto);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet("GetDetailsOfUser")]
        [Authorize]
        public ActionResult<UsersWithDetailsView> GetDetailsOfUser(int userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is not null)
            {
                int userIdFromToken = Convert.ToInt32(identity.FindFirst(ClaimTypes.Actor)?.Value);
                if (userIdFromToken != userId)
                {
                    return Unauthorized();
                }
                using (UnitOfWork UoW = new UnitOfWork(userIdFromToken))
                {
                    var result = UoW.Users.GetDetailsOfUser(userId);
                    if (result is not null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("Register")]
        public ActionResult<bool> RegisterNewUser(RegisterUserDto user)
        {
            using (UnitOfWork UoW = new UnitOfWork())
            {
                var result = UoW.Users.RegisterNewUser(user);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPatch]
        [Authorize]
        public ActionResult UpdateUsersDetails(UsersWithDetailsView newUser)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is not null)
            {
                int userIdFromToken = Convert.ToInt32(identity.FindFirst(ClaimTypes.Actor)?.Value);
                using (UnitOfWork UoW = new UnitOfWork(userIdFromToken))
                {
                    var result = UoW.Users.UpdateUserDetails(newUser);
                    if (result)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return Unauthorized();
        }
            
        #endregion

        #region Private Helpers
        /// <summary>
        /// Generates a JSON Web Token for the user
        /// </summary>
        /// <param name="userId">The users Id from the DB</param>
        /// <returns>The geenrated token that has a claim (actor) with the users id</returns>
        private string CreateToken(int userId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Actor, userId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddDays(1)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion
    }
}

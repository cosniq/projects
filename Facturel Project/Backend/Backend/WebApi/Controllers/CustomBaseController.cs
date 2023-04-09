using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize]
    public class CustomBaseController : ControllerBase
    {
        internal int UserIdFromToken
        {
            get
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity is not null)
                {
                    var claim = identity.FindFirst(ClaimTypes.Actor);
                    if (claim is not null)
                    {
                        return Convert.ToInt32(claim.Value);
                    }
                    else
                    {
                        throw new HttpRequestException(HttpStatusCode.Unauthorized.ToString());
                    }
                }
                else
                {
                    throw new HttpRequestException(HttpStatusCode.Unauthorized.ToString());
                }
            }
        }
    }
}

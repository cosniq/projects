using BusinessLayer;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthsController : CustomBaseController
    {
        [HttpPost]
        public ActionResult GenerateNewMonthForLocation(string locationDescription)
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                if (UoW.Months.GenerateNewMonthForLocation(locationDescription))
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
        public ActionResult UpdateAMonthsDescription(UpdateMonthNameDto updateMonthNameDto)
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                if (UoW.Months.UpdateOnesName(updateMonthNameDto))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}

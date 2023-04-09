using DataTransferObjects;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : CustomBaseController
    {
        [HttpPatch]
        public ActionResult UpdateACountersSerialNumber(CounterDto counter)
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                var result = UoW.Counters.UpdateSerialNumber(counter);
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

        [HttpGet]
        public ActionResult<List<CounterWithLocationAndTypeDto>> GetAllCounters()
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                return Ok(UoW.Counters.GetAllCounters());
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataTransferObjects;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterTypeController : CustomBaseController
    {
        [HttpPost]
        public ActionResult InsertNew([FromBody] CounterTypeDto newCounterType)
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                var result = UoW.CounterTypes.AddNewCounterType(newCounterType);
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
        public ActionResult<List<CounterTypeDto>> GetAll()
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                return Ok(UoW.CounterTypes.GetAll());
            }
        }

        [HttpPatch]
        public ActionResult UpdateOne([FromBody] CounterTypeDto newCounter)
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                var result = UoW.CounterTypes.UpdateOne(newCounter);
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
    }
}

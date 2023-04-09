using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using RepositoryLayer;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasurementsController : CustomBaseController
    {
        [HttpGet]
        public ActionResult<List<UnitsOfMeasurementView>> GetUnitsOfMeasurement()
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                return Ok(UoW.UnitsOfMeasurement.GetUnitsOfMeasurement());
            }
        }

        [HttpPost]
        public ActionResult InsertNewUnitOfMeasurement(string symbol)
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                var result = UoW.UnitsOfMeasurement.InsertNewUnitOfMeasurement(symbol);
                if (result is false)
                {
                    return BadRequest(); // UoM already exists
                }
                return NoContent();
            }
        }
    }
}


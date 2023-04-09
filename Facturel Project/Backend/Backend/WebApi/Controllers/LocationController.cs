using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using RepositoryLayer;
using DataTransferObjects;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : CustomBaseController
    {
        [HttpGet]
        public ActionResult<ViewModelDto> GetLocationViewModel()
        {
            using(UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                return Ok(UoW.Locations.GetLocationViewModel(UserIdFromToken));
            }
        }

        [HttpPost]
        public ActionResult InsertNewLocationForUser([FromBody] LocationsView location)
        {
            if (location.UserId != UserIdFromToken) // trying to insert location for another user
            {
                return Unauthorized();
            }
            else
            {
                bool result;
                using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
                {
                    result = UoW.Locations.InsertNewLocationForUser(location);
                }
                return result ? NoContent() : BadRequest(); // false means that the location already exists
            }
        }

        [HttpPatch]
        public ActionResult UpdateLocationForUser([FromBody] LocationsView location)
        {
            if (location.UserId != UserIdFromToken) // trying to update location for another user
            {
                return Unauthorized();
            }
            else
            {
                bool result;
                using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
                {
                    result = UoW.Locations.UpdateLocationOfUser(location);
                }
                return result ? NoContent() : NotFound(); // false means that the location wasn't found
            }
        }

        [HttpPost("GetIdOfLocation")]
        public ActionResult<int> GetLocationsId([FromBody] LocationsView location)
        {
            using (UnitOfWork UoW = new(UserIdFromToken))
            {
                var result = UoW.Locations.GetIdOfLocation(location);
                if (result is not null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut]
        public ActionResult Submit([FromBody] ViewModelDto viewModel)
        {
            if (viewModel.UserId != UserIdFromToken) // trying to submit for another user
            {
                return Unauthorized();
            }
            else
            {
                bool result;
                using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
                {
                    result = UoW.Locations.Submit(viewModel);
                }
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Some changes might have been saved");
                }
            }
        }
    }
}

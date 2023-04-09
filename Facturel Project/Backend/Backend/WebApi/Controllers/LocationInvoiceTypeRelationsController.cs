using BusinessLayer;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationInvoiceTypeRelationsController : CustomBaseController
    {
        [HttpGet]
        public ActionResult<List<LocationInvoiceTypeRelationDto>> GetAllLocationInvoiceTypeRelations()
        {
            using(UnitOfWork unitOfWork = new(UserIdFromToken))
            {
                return Ok(unitOfWork.LocationInvoiceTypeRelations.GetAllLocationInvoiceTypeRelations());
            }
        }

        [HttpDelete]
        public ActionResult MarkRelationAsInactive(int relationId)
        {
            using (UnitOfWork unitOfWork = new(UserIdFromToken))
            {
                if (unitOfWork.LocationInvoiceTypeRelations.MarkLinkInactive(relationId))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult InsertNewRelation([FromBody] LocationInvoiceTypeRelationDto relation)
        {
            using (UnitOfWork unitOfWork = new(UserIdFromToken))
            {
                if (unitOfWork.LocationInvoiceTypeRelations.AddOneLink(relation))
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

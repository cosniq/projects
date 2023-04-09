using BusinessLayer;
using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceTypeController : CustomBaseController
    {
        [HttpGet]
        public ActionResult<List<InvoiceTypeDto>> GetAll()
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                return Ok(UoW.InvoiceTypes.GetAll());
            }
        }

        [HttpPost]
        public ActionResult InsertInvoiceType([FromBody] InvoiceTypeDto invoiceType)
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                var result = UoW.InvoiceTypes.InsertInvoiceType(invoiceType);
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
        public ActionResult UpdateInvoiceType([FromBody] InvoiceTypeDto invoiceType)
        {
            using (UnitOfWork UoW = new UnitOfWork(UserIdFromToken))
            {
                var result = UoW.InvoiceTypes.UpdateOneInvoiceType(invoiceType);
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

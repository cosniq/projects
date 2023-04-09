using RepositoryLayer;
using DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class InvoiceTypes : Base
    {
        #region Constructor
        public InvoiceTypes(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Gets every invoice type from the DB
        /// </summary>
        /// <returns>A list of invoice types from the db with the associated counter types name</returns>
        public List<InvoiceTypeDto> GetAll()
        {
            var invoiceTypes = DbContext.InvoiceTypesViews.ToList();
            var counterTypes = DbContext.CounterTypesViews.ToList();
            return mapper.Map(invoiceTypes, counterTypes);
        }

        /// <summary>
        /// Inserts a new counter type into the db
        /// </summary>
        /// <param name="invoiceType">The new invoice type with the info</param>
        /// <returns>True if succeeded, False if the name was already taken or the counterType wasn't found</returns>
        public bool InsertInvoiceType(InvoiceTypeDto invoiceType)
        {
            var invoiceTypeFromDb = DbContext.InvoiceTypesViews.Where(it => it.Name == invoiceType.Name).SingleOrDefault();
            if (invoiceTypeFromDb is not null)
            {
                return false;
            }

            var counterTypeFromDb = DbContext.CounterTypesViews.Where(ct => ct.Name == invoiceType.CounterType).SingleOrDefault();

            if (counterTypeFromDb is null)
            {
                return false;
            }

            var param1 = new SqlParameter("@InvoiceType", invoiceType.Name);
            var param2 = new SqlParameter("@CounterType", counterTypeFromDb.Id);
            var param3 = new SqlParameter("@CostOnlyDependentOnUsage", invoiceType.CostOnlyDependentOnUsage);

            DbContext.Database.ExecuteSqlRaw("INSERT INTO Invoice_Types_View(Name, Counter_Type, Cost_only_dependent_on_usage) VALUES (@InvoiceType, @CounterType, @CostOnlyDependentOnUsage)", param1, param2, param3);
            return true;
        }


        /// <summary>
        /// Updates an InvoiceType
        /// </summary>
        /// <param name="newInvoiceType">The invoiceType with the new data but with the old Id</param>
        /// <returns>True if succeeded, False if this Id doesn't exist OR the counter type wasn't found</returns>
        public bool UpdateOneInvoiceType(InvoiceTypeDto newInvoiceType)
        {
            var invoiceTypeFromDb = DbContext.InvoiceTypesViews.Where(it => it.Id == newInvoiceType.Id).SingleOrDefault();
            if (invoiceTypeFromDb is null)
            {
                return false;
            }

            var counterTypeFromDb = DbContext.CounterTypesViews.Where(ct => ct.Name == newInvoiceType.CounterType).SingleOrDefault();

            if (counterTypeFromDb is null)
            {
                return false;
            }

            var param0 = new SqlParameter("@Id", newInvoiceType.Id);
            var param1 = new SqlParameter("@InvoiceType", newInvoiceType.Name);
            var param2 = new SqlParameter("@CounterType", counterTypeFromDb.Id);
            var param3 = new SqlParameter("@CostOnlyDependentOnUsage", newInvoiceType.CostOnlyDependentOnUsage);

            DbContext.Database.ExecuteSqlRaw("UPDATE Invoice_Types_View SET Name=@InvoiceType, Counter_Type=@CounterType, Cost_only_dependent_on_usage=@CostOnlyDependentOnUsage WHERE Id=@Id", param1, param2, param3, param0);
            return true;
        }
        #endregion
    }
}

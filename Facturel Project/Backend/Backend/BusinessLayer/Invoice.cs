using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using DataTransferObjects;

namespace BusinessLayer
{
    public class Invoice : Base
    {
        #region Fields
        List<InvoicesView> listOfInvoicesToBeAdded = new List<InvoicesView>();
        IndexReadings indexReadings;
        #endregion

        #region Constructors
        public Invoice(TIAProjContext dbContext, IndexReadings indexReadings) : base(dbContext)
        {
            this.indexReadings = indexReadings;
        }
        #endregion

        #region Internal Functions

        internal bool Submit(int monthId, List<InvoicesVMDto> invoices)
        {
            // get the invoices from the db
            foreach (var invoice in invoices)
            {
                // get invoice type id -> needed for localization of element
                var invoiceType = DbContext.InvoiceTypesViews.Where(i => i.Name == invoice.InvoiceTypeName).SingleOrDefault();

                if (invoiceType is null)
                {
                    return false;
                }

                var invoiceFromDb = DbContext.InvoicesViews.Where(i => i.InvoiceType == invoiceType.Id && i.Month == monthId).SingleOrDefault();
                if (invoiceFromDb is null)
                {
                    return false;
                }
                else
                {
                    // Update underlying ViewModel
                    indexReadings.Submit(invoiceFromDb.Id, invoice.IndexReading);

                    // Update the price of the invoice
                    if (invoice.Price != invoiceFromDb.Price)
                    {
                        var param1 = new SqlParameter("@Id", invoiceFromDb.Id);
                        var param2 = new SqlParameter("@Price", invoice.Price);
                        DbContext.Database.ExecuteSqlRaw("UPDATE Invoices_View SET Price = @Price WHERE Id = @Id", param2, param1);
                    }

                    // Update the paid status of the invoice
                    if (invoice.Paid != invoiceFromDb.Paid)
                    {
                        var param1 = new SqlParameter("@Id", invoiceFromDb.Id);
                        var param2 = new SqlParameter("@Paid", invoice.Paid);
                        DbContext.Database.ExecuteSqlRaw("UPDATE Invoices_View SET Paid = @Paid WHERE Id = @Id", param2, param1);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Creates Invoice Objects for a month based on the InvoiceTypes attached to the location
        /// </summary>
        /// <param name="monthId">The months id</param>
        /// <param name="locationId">The locations id</param>
        /// <returns>False if no invoice types are attached to the location OR if no counters were found for an invoicetype OR there was an exception</returns>
        internal bool CreateInvoicesForMonth(int monthId, int locationId)
        {
            try
            {
                // Get InvoiceTypes attached to the location
                var invoiceTypeIds = DbContext.LocInvTypeViews.Where(lit => lit.Location == locationId && lit.Active == true).Select(lit => lit.InvoiceType).ToList();
                if (invoiceTypeIds is null)
                {
                    return false;
                }

                var invoiceTypes = DbContext.InvoiceTypesViews.Where(it => invoiceTypeIds.Contains(it.Id)).ToList();
                if (invoiceTypes is null)
                {
                    return false;
                }

                // Go through every invoice type and generate new invoices based off of them
                foreach (var invoiceType in invoiceTypes)
                {
                    // Get the counter for this invoice type at this location
                    var counter = DbContext.CountersViews.Where(c => c.Location == locationId && c.CounterType == invoiceType.CounterType).SingleOrDefault();

                    if (counter is null)
                    {
                        return false;
                    }

                    // Generate invoices based on those invoice types
                    var newInvoice = new InvoicesView()
                    {
                        Id = 0,
                        Counter = counter.Id,
                        Month = monthId,
                        InvoiceType = invoiceType.Id,
                        Price = null,
                        Paid = false
                    };

                    // Array to which holds the elements that need to be added into the db
                    // They are not pushed instantly so that in case a counter isn't found we can annul the operations
                    listOfInvoicesToBeAdded.Add(newInvoice);
                }

                // Save into the db and get their generated ids
                foreach (var invoiceToBeAdded in listOfInvoicesToBeAdded)
                {
                    AddIntoDb(invoiceToBeAdded);

                    var aux = DbContext.InvoicesViews.Where(i => i.InvoiceType == invoiceToBeAdded.InvoiceType && i.Counter == invoiceToBeAdded.Counter && i.Month == invoiceToBeAdded.Month && i.Price == invoiceToBeAdded.Price).SingleOrDefault();
                    if (aux is null) // if something went wrong -> revert changes
                    {
                        revertChanges();
                        listOfInvoicesToBeAdded.Clear();
                        return false;
                    }

                    invoiceToBeAdded.Id = aux.Id;
                }

                if (!indexReadings.GenerateIndexReadingsForInvoices(listOfInvoicesToBeAdded))
                {
                    revertChanges();
                    listOfInvoicesToBeAdded.Clear();
                    return false;
                }

                listOfInvoicesToBeAdded.Clear(); // only store modifications during current instance
                return true;
            }
            catch
            {
                revertChanges();
                return false;
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Function that adds into the db an invoice
        /// </summary>
        /// <param name="invoice">The invoice to be added into the db</param>
        private void AddIntoDb(InvoicesView invoice)
        {
            var param1 = new SqlParameter("@Counter", invoice.Counter);
            var param2 = new SqlParameter("@Month", invoice.Month);
            var param3 = new SqlParameter("@InvoiceType", invoice.InvoiceType);
            var param4 = new SqlParameter("@Paid", invoice.Paid);

            DbContext.Database.ExecuteSqlRaw("INSERT INTO Invoices_View(Counter, Month, Invoice_Type, Paid) VALUES (@Counter, @Month, @InvoiceType, @Paid)", param1, param2, param3, param4);
        }

        /// <summary>
        /// Deletes from the db the newly added elements
        /// </summary>
        private void revertChanges()
        {
            var elementsToBeDeleted = listOfInvoicesToBeAdded.Where(i => i.Id != 0).ToList(); // get the elements that were successfully saved -> they are the ones that need to be deleted
            foreach(var elementToDelete in elementsToBeDeleted)
            {
                var param = new SqlParameter("@Id", elementToDelete.Id);
                DbContext.Database.ExecuteSqlRaw("DELETE FROM Invoices_View WHERE Id=@Id", param);
            }
        }

        #endregion
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using DataTransferObjects;

namespace BusinessLayer
{
    public class IndexReadings : Base
    {
        #region Fields
        List<IndexReadingsView> listOfIndexReadingsToBeAdded = new List<IndexReadingsView>();
        #endregion

        #region Constructors
        public IndexReadings(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Internal Functions

        /// <summary>
        /// Submits the changes done to the ViewModel into the Db
        /// </summary>
        /// <param name="invoiceId">The associated invoice's Id</param>
        /// <param name="indexReadings">The IndexReading ViewModel</param>
        /// <returns>True if successfull, False if something went wrong</returns>
        internal bool Submit(int invoiceId, IndexReadingWithUnitDto indexReading)
        {
            // get the index readings from the db
            var indexReadingFromDb = DbContext.IndexReadingsViews.Where(i => i.Invoice == invoiceId).SingleOrDefault();
            if (indexReadingFromDb is null)
            {
                return false;
            }
            else
            {
                // Update the value of the index reading
                if (indexReading.IndexReading != indexReadingFromDb.NumberOnCounter)
                {
                    var param1 = new SqlParameter("@Id", indexReadingFromDb.Id);
                    var param2 = new SqlParameter("@Value", indexReading.IndexReading);
                    DbContext.Database.ExecuteSqlRaw("UPDATE Index_Readings_View SET Number_on_Counter = @Value WHERE Id = @Id", param2, param1);
                }

                // Update the date of the index reading
                if (indexReading.DateOfReading != indexReadingFromDb.DateOfReading)
                {
                    var param1 = new SqlParameter("@Id", indexReadingFromDb.Id);
                    var param2 = new SqlParameter("@Date", indexReading.DateOfReading);
                    DbContext.Database.ExecuteSqlRaw("UPDATE Index_Readings_View SET Date_of_Reading = @Date WHERE Id = @Id", param2, param1);
                }
            }

            return true;
        }

        /// <summary>
        /// Generates Index Reading objects in the db for invoices
        /// </summary>
        /// <param name="invoices">The invoices for which Index Readings should be generated</param>
        /// <returns>True if successfull, False if something went wrong</returns>
        internal bool GenerateIndexReadingsForInvoices(List<InvoicesView> invoices)
        {
            try
            {
                foreach (var invoice in invoices)
                {
                    var elemToBeAdded = new IndexReadingsView()
                    {
                        Id = 0,
                        NumberOnCounter = 0,
                        Counter = invoice.Counter,
                        DateOfReading = DateTime.Now,
                        Invoice = invoice.Id
                    };

                    listOfIndexReadingsToBeAdded.Add(elemToBeAdded);
                }

                foreach (var elem in listOfIndexReadingsToBeAdded)
                {
                    AddIntoAdd(elem);

                    var elemFromDb = DbContext.IndexReadingsViews.Where(ir => ir.Invoice == elem.Invoice && ir.Counter == elem.Counter && ir.DateOfReading == elem.DateOfReading && ir.NumberOnCounter == elem.NumberOnCounter).SingleOrDefault();
                    if (elemFromDb is null) // something went wrong
                    {
                        revertChanges();
                        listOfIndexReadingsToBeAdded.Clear();
                        return false;
                    }

                    elem.Id = elemFromDb.Id;
                }

                listOfIndexReadingsToBeAdded.Clear();
                return true;
            }
            catch
            {
                revertChanges(); // revert every change made to the db in case there was an exception
                return false;
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Function that adds an IndexReading object into the db
        /// </summary>
        /// <param name="elem">The object to be added into the db</param>
        private void AddIntoAdd(IndexReadingsView elem)
        {
            var param1 = new SqlParameter("@NrOnCounter", elem.NumberOnCounter);
            var param2 = new SqlParameter("@Counter", elem.Counter);
            var param3 = new SqlParameter("@Date", elem.DateOfReading);
            var param4 = new SqlParameter("@Invoice", elem.Invoice);

            DbContext.Database.ExecuteSqlRaw("INSERT INTO Index_Readings_View(Number_on_Counter, Date_of_Reading, Counter, Invoice) VALUES (@NrOnCounter, @Date, @Counter, @Invoice)", param1, param3, param2, param4);
        }

        /// <summary>
        /// Deletes from the db the newly added elements
        /// </summary>
        private void revertChanges()
        {
            var elementsToBeDeleted = listOfIndexReadingsToBeAdded.Where(ir => ir.Id != 0).ToList();
            foreach(var elem in elementsToBeDeleted)
            {
                var param = new SqlParameter("@Id", elem.Id);
                DbContext.Database.ExecuteSqlRaw("DELETE FROM Index_Readings_View WHERE Id=@Id", param);
            }
        }

        #endregion
    }
}

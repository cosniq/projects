using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace BusinessLayer
{
    public class UnitOfWork : IDisposable
    {
        #region Fields
        public Users Users;
        public Locations Locations;
        public InvoiceTypes InvoiceTypes;
        public UnitOfMeasurements UnitsOfMeasurement;
        public CounterTypes CounterTypes;
        public Counters Counters;
        private IndexReadings IndexReadings;
        private Invoice Invoices;
        public Months Months;
        public LocationInvoiceTypeRelations LocationInvoiceTypeRelations;
        internal TIAProjContext DbContext;
        #endregion

        #region Constructors
        /// <summary>
        /// Generates a DbConnection that can be used, but session context is not set -> most data cannot be accessed
        /// </summary>
        public UnitOfWork()
        {
            OpenDbConnection();
            CreateFields();
        }
        
        /// <summary>
        /// Generates a DbConnection where the session context is set -> necessary to access most data
        /// </summary>
        /// <param name="userId">The users id as it is in the DB</param>
        public UnitOfWork(int userId)
        {
            OpenDbConnection();
            DbContext.Database.ExecuteSqlRaw($"EXEC sp_set_session_context 'user_id', {userId}");
            CreateFields();
        }
        #endregion

        #region Destructors
        /// <summary>
        /// Disposes the object -> deletes the session context, closes the connection to the db
        /// </summary>
        public void Dispose()
        {
            DbContext.Database.ExecuteSqlRaw("EXEC sp_set_session_context 'user_id', NULL");
            DbContext.Database.GetDbConnection().Close();
            DbContext.Dispose();
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Defines the DbContext and opens the connection to the DB
        /// </summary>
        private void OpenDbConnection() 
        {
            DbContext = new TIAProjContext();
            DbContext.Database.GetDbConnection().Open();
        }

        /// <summary>
        /// Creates every field and passes the DbContext to their constructor -> in these classes the session context would already be set
        /// </summary>
        private void CreateFields()
        {
            Users = new Users(DbContext);
            InvoiceTypes = new InvoiceTypes(DbContext);
            UnitsOfMeasurement = new UnitOfMeasurements(DbContext);
            CounterTypes = new CounterTypes(DbContext);
            Counters = new Counters(DbContext);
            IndexReadings = new IndexReadings(DbContext);
            Invoices = new Invoice(DbContext, IndexReadings);
            Months = new Months(DbContext, Invoices);
            Locations = new Locations(DbContext, Months);
            LocationInvoiceTypeRelations = new LocationInvoiceTypeRelations(DbContext);
        }
        #endregion
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using DataTransferObjects;

namespace BusinessLayer
{
    public class Months : Base
    {
        #region Fields
        Invoice invoices;
        MonthsView? monthToBeAdded;
        #endregion

        #region Constructors
        public Months(TIAProjContext dbContext, Invoice invoices) : base(dbContext)
        {
            this.invoices = invoices;
            this.monthToBeAdded = null;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Submits the changes done to the ViewModel into the Db
        /// </summary>
        /// <param name="locationId">The associated location's Id</param>
        /// <param name="months">ViewModel of the Months</param>
        /// <returns>True if succeeded, False if something went wrong</returns>
        public bool Submit(int locationId, List<MonthsVMDto> months)
        {
            // get the months from the db
            foreach(var month in months)
            {
                var monthFromDb = DbContext.MonthsViews.Where(m => m.Description == month.Description && m.Location == locationId).SingleOrDefault();
                if (monthFromDb is null)
                {
                    return false;
                }
                else
                {
                    invoices.Submit(monthFromDb.Id, month.Invoices);
                }
            }
            return true;
        }

        /// <summary>
        /// Updates a Month's description
        /// </summary>
        /// <param name="updateMonth">An object that contains the month's id and new description</param>
        /// <returns>True if successfull, False if it wasn't found</returns>
        public bool UpdateOnesName(UpdateMonthNameDto updateMonth)
        {
            var locationFromDb = DbContext.LocationsViews.Where(l => l.Description == updateMonth.LocationDescription).SingleOrDefault();
            if (locationFromDb is null)
            {
                return false;
            }

            var elemFromDb = DbContext.MonthsViews.Where(m => m.Description == updateMonth.OldDescription && m.Location == locationFromDb.Id).SingleOrDefault();
            if (elemFromDb is null)
            {
                return false;
            }

            var alreadyInDb = DbContext.MonthsViews.Where(m => m.Description == updateMonth.NewDescription && m.Location == locationFromDb.Id).SingleOrDefault();
            if (alreadyInDb is not null)
            {
                return false;
            }

            var param1 = new SqlParameter("@Description", updateMonth.NewDescription);
            var param2 = new SqlParameter("@Id", elemFromDb.Id);
            DbContext.Database.ExecuteSqlRaw("UPDATE Months_View SET Description = @Description WHERE Id = @Id", param1, param2);
            return true;
        }

        /// <summary>
        /// Generates a new month for a location
        /// </summary>
        /// <param name="locationId">The location's id</param>
        /// <returns>True if succeeded, False if something went wrong (e.g. Location already has a month objects for this month)</returns>
        public bool GenerateNewMonthForLocation(string locationDescription)
        {
            try
            {
                var locationFromDb = DbContext.LocationsViews.Where(l => l.Description == locationDescription).SingleOrDefault();
                if (locationFromDb == null)
                {
                    return false;
                }

                monthToBeAdded = new MonthsView()
                {
                    Id = 0,
                    Location = locationFromDb.Id,
                    Description = DateTime.Now.ToString("yyyy MMMM"),
                };

                var alreadyInDb = DbContext.MonthsViews.Where(m => m.Description == monthToBeAdded.Description && m.Location == monthToBeAdded.Location).SingleOrDefault();

                if (alreadyInDb is not null)
                {
                    return false;
                }

                AddIntoDb(monthToBeAdded);

                var elemInDb = DbContext.MonthsViews.Where(m => m.Description == monthToBeAdded.Description && m.Location == monthToBeAdded.Location).SingleOrDefault();
                if (elemInDb is null)
                {
                    return false;
                }

                monthToBeAdded.Id = elemInDb.Id;

                if (!invoices.CreateInvoicesForMonth(monthToBeAdded.Id, locationFromDb.Id))
                {
                    revertChanges();
                    return false;
                }

                monthToBeAdded = null;
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
        /// Checks if a month object already exists for the current month and if not it creates one
        /// </summary>
        /// <param name="month">The month object to be checked and added</param>
        public void AddIntoDb(MonthsView month)
        {
            var elemFromDb = DbContext.MonthsViews.Where(m => m.Description == month.Description && m.Location == month.Location).SingleOrDefault();
            if (elemFromDb is not null)
            {
                return;
            }

            var param1 = new SqlParameter("@Description", month.Description);
            var param2 = new SqlParameter("@Location", month.Location);

            DbContext.Database.ExecuteSqlRaw("INSERT INTO Months_View(Description, Location) VALUES (@Description, @Location)", param1, param2);
        }

        /// <summary>
        /// Reverts the changes made to the db
        /// </summary>
        private void revertChanges()
        {
            if (monthToBeAdded is not null && monthToBeAdded.Id != 0)
            {
                var param = new SqlParameter("@Id", monthToBeAdded.Id);

                DbContext.Database.ExecuteSqlRaw("DELETE FROM Months_View WHERE Id=@Id", param);
            }
        }

        #endregion
    }
}

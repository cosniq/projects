using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using DataTransferObjects;

namespace BusinessLayer
{
    public class Locations : Base
    {
        #region Fields
        private Months Months;
        #endregion

        #region Constructors
        public Locations(TIAProjContext dbContext, Months Months) : base(dbContext)
        {
            this.Months = Months;
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Updates every variable in the DB
        /// </summary>
        /// <param name="viewModel">A ViewModel with every updated information</param>
        /// <returns>True if succeeded, False if something went wrong</returns>
        public bool Submit(ViewModelDto viewModel)
        {
            var locationsFromDb = new List<LocationsView>();
            // parse through the locations
            foreach (var location in viewModel.Locations)
            {
                // find location in the db
                var locationFromDb = DbContext.LocationsViews.Where(l => l.Address==location.Address && l.Description==location.Description).SingleOrDefault();
                if (locationFromDb is null)
                {
                    return false;
                }
                else
                {
                    // Update it's data
                    Months.Submit(locationFromDb.Id, location.Months);
                }
            }
            return true;
        }

        /// <summary>
        /// Searches for the locations of the user and builds up a complete ViewModel for the frontend
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>A List of Locations with every associated data (every months locations, every invoice for months, every counter for invoices, every index reading for counters, every unit of measurement for index readings)</returns>
        public ViewModelDto GetLocationViewModel(int userId)
        {
            var locations = DbContext.LocationsViews.ToList(); // locations of a user (access is restricted by the db to only the user's own locations)
            var months = DbContext.MonthsViews.ToList(); // months of a user (access is restricted by the db to only the user's own months)
            var invoices = DbContext.InvoicesViews.ToList(); // invoices of a user (access is restricted by the db to only the user's own invoices)
            var counters = DbContext.CountersViews.ToList(); // counters of a user (access is restricted by the db to only the user's own counters)
            var indexReadings = DbContext.IndexReadingsViews.ToList(); // index readings of a user (access is restricted by the db to only the user's own index readings)
            var units = DbContext.UnitsOfMeasurementViews.ToList(); // units of measurement of a user (access is restricted by the db to only the user's own units of measurement)
            var counterTypes = DbContext.CounterTypesViews.ToList(); // counter types of a user (access is restricted by the db to only the user's own counter types)
            var invoiceTypes = DbContext.InvoiceTypesViews.ToList(); // invoice types of a user (access is restricted by the db to only the user's own invoice types)

            return mapper.Map(userId, locations, months, invoices, counters, indexReadings, units, counterTypes, invoiceTypes);
        }
        
        /// <summary>
        /// Inserts a location for a user into the db
        /// </summary>
        /// <param name="location">The location to be inserted</param>
        /// <returns>True if successfull, false if it already is in the db</returns>
        public bool InsertNewLocationForUser(LocationsView location)
        {
            var alreadyExisting = DbContext.LocationsViews.Where(l => l.Description == location.Description).SingleOrDefault();
            if (alreadyExisting is not null)
            {
                return false;
            }
            else
            {
                var param1 = new SqlParameter("@Address", location.Address);
                var param2 = new SqlParameter("@Description", location.Description);
                var param3 = new SqlParameter("@UserId", location.UserId);
                DbContext.Database.ExecuteSqlRaw("INSERT INTO Locations_View(Address, Description, User_Id) VALUES (@Address, @Description, @UserId)", param1, param2, param3);
                DbContext.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Updates a location
        /// </summary>
        /// <param name="updatedLocation">The location with the updated parameters</param>
        /// <returns>True if succeeded, False if not found</returns>
        public bool UpdateLocationOfUser(LocationsView updatedLocation)
        {
            var locationInDb = DbContext.LocationsViews.Where(l => l.Id == updatedLocation.Id).SingleOrDefault();
            if (locationInDb is not null)
            {
                var alreadyExisting = DbContext.LocationsViews.Where(l => l.Description == updatedLocation.Description).SingleOrDefault();
                if (alreadyExisting is not null)
                {
                    return false;
                }

                var param1 = new SqlParameter("@Address", updatedLocation.Address);
                var param2 = new SqlParameter("@Description", updatedLocation.Description);
                var param3 = new SqlParameter("@Id", updatedLocation.Id);
                DbContext.Database.ExecuteSqlRaw("UPDATE Locations_View SET Address=@Address, Description=@Description WHERE Id=@Id", param1, param2, param3);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the Id of a Location from the Db
        /// </summary>
        /// <param name="location">The location</param>
        /// <returns>The Id if found, null if it wasn't found</returns>
        public int? GetIdOfLocation(LocationsView location)
        {
            var locationInDb = DbContext.LocationsViews.Where(l => l.Description == location.Description && l.Address == location.Address && l.UserId == location.UserId).SingleOrDefault();
            if (locationInDb is not null)
            {
                return locationInDb.Id;
            }
            return null;
        }

        #endregion
    }
}

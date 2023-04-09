using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace BusinessLayer
{
    public class UnitOfMeasurements : Base
    {
        #region Constructors
        public UnitOfMeasurements(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Gets every Unit of Measurement in the database
        /// </summary>
        /// <returns>A list of unit of measurements</returns>
        public List<UnitsOfMeasurementView> GetUnitsOfMeasurement()
        {
            return DbContext.UnitsOfMeasurementViews.ToList();
        }

        /// <summary>
        /// Inserts a new unit of measurement into the db
        /// </summary>
        /// <param name="symbol">The new Unit Of Measurement's symbol</param>
        /// <returns>True if successfull, False is UoM already exists</returns>
        public bool InsertNewUnitOfMeasurement(string symbol)
        {
            var alreadyInDb = DbContext.UnitsOfMeasurementViews.Where(uom => uom.Symbol == symbol).SingleOrDefault();
            if (alreadyInDb is not null)
            {
                return false;
            }

            var param = new SqlParameter("@Symbol", symbol);
            DbContext.Database.ExecuteSqlRaw("INSERT INTO Units_of_measurement_View(Symbol) VALUES (@Symbol)", param);
            return true;
        }

        #endregion
    }
}

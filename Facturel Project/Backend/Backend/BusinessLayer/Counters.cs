using RepositoryLayer;
using DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class Counters : Base
    {
        #region Constructors
        public Counters(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Gets all counters in the database.
        /// </summary>
        /// <returns>A List of Counters</returns>
        public List<CounterWithLocationAndTypeDto> GetAllCounters()
        {
            return mapper.Map(DbContext.CountersViews.ToList(), DbContext.LocationsViews.ToList(), DbContext.CounterTypesViews.ToList());
        }
        
        /// <summary>
        /// Updates a Counters Serial Number
        /// </summary>
        /// <param name="counter">An object that contains the counters Id and Serial Number</param>
        /// <returns>True if succeeded, False if the counter wasn't found</returns>
        public bool UpdateSerialNumber(CounterDto counter)
        {
            var counterFromDb = DbContext.CountersViews.Where(c => c.Id == counter.Id).SingleOrDefault();
            if (counterFromDb is null)
            {
                return false;
            }

            var param1 = new SqlParameter("@Id", counter.Id);
            var param2 = new SqlParameter("@SerialNr", counter.SerialNumber);

            DbContext.Database.ExecuteSqlRaw("UPDATE Counters_View SET Serial_Nr=@SerialNr WHERE Id=@Id", param2, param1);
            return true;
        }
        
        #endregion
    }
}

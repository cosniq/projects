using RepositoryLayer;
using DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class CounterTypes : Base
    {
        #region Constructors
        public CounterTypes(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Adds a new Counter Type into the DB
        /// </summary>
        /// <param name="newCounter">A Counter Type Dto</param>
        /// <returns>True if succeeded, False if a counter type already exists with this name</returns>
        public bool AddNewCounterType (CounterTypeDto newCounter)
        {
            var elemFromDb = DbContext.CounterTypesViews.Where(ct => ct.Name == newCounter.Name).SingleOrDefault();
            if (elemFromDb is not null)
            {
                return false;
            }
            var UoMId = DbContext.UnitsOfMeasurementViews.Where(u => u.Symbol == newCounter.UnitOfMeasurement).SingleOrDefault()?.Id;
            if (UoMId is null)
            {
                return false;
            }
            var param1 = new SqlParameter("@Name", newCounter.Name);
            var param2 = new SqlParameter("@UnitOfMeasurement", UoMId);
            DbContext.Database.ExecuteSqlRaw("INSERT INTO Counter_Types_View(Name, Unit_of_measurement) VALUES (@Name, @UnitOfMeasurement)", param1, param2);
            return true;
        }

        /// <summary>
        /// Gets every counter type from the db
        /// </summary>
        /// <returns>A list with these counter types</returns>
        public List<CounterTypeDto> GetAll()
        {
            return mapper.Map(DbContext.CounterTypesViews.ToList(), DbContext.UnitsOfMeasurementViews.ToList());
        }

        /// <summary>
        /// Updates a Counter Type
        /// </summary>
        /// <param name="newCounter">The Counter Type with the updated data</param>
        /// <returns>True if successfull, False if it wasn't found</returns>
        public bool UpdateOne(CounterTypeDto newCounter)
        {
            var oldCounter = DbContext.CounterTypesViews.Where(ct => ct.Id == newCounter.Id).SingleOrDefault();
            if (oldCounter is null)
            {
                return false;
            }
            var UoMId = DbContext.UnitsOfMeasurementViews.Where(u => u.Symbol == newCounter.UnitOfMeasurement).SingleOrDefault()?.Id;
            if (UoMId is null)
            {
                return false;
            }
            var param1 = new SqlParameter("@Name", newCounter.Name);
            var param2 = new SqlParameter("@UnitOfMeasurement", UoMId);
            var param3 = new SqlParameter("@Id", oldCounter.Id);
            DbContext.Database.ExecuteSqlRaw("UPDATE Counter_Types_View SET Name=@Name, Unit_of_measurement=@UnitOfMeasurement WHERE Id=@Id", param1, param2, param3);
            return true;
        }

        #endregion
    }
}

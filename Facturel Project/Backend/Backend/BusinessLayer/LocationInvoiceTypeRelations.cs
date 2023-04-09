using RepositoryLayer;
using DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class LocationInvoiceTypeRelations : Base
    {
        #region Constructors
        public LocationInvoiceTypeRelations(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Gets every Location-InvoiceType relation from the db
        /// </summary>
        /// <returns>A list of those relations. If nothing was found an empty list will be returned</returns>
        public List<LocationInvoiceTypeRelationDto> GetAllLocationInvoiceTypeRelations()
        {
            return mapper.Map(DbContext.LocInvTypeViews.Where(lit => lit.Active == true).ToList(), DbContext.LocationsViews.ToList(), DbContext.InvoiceTypesViews.ToList());
        }

        /// <summary>
        /// Marks a relation inactive
        /// </summary>
        /// <param name="linkId">The relation's id that should be marked as inactive</param>
        /// <returns>True if succeeded, False if the relation wasn't found</returns>
        public bool MarkLinkInactive(int linkId)
        {
            var elemFromDb = DbContext.LocInvTypeViews.Where(lit => lit.Id == linkId).SingleOrDefault();
            if (elemFromDb is null)
            {
                return false;
            }

            var param = new SqlParameter("@Id", linkId);
            DbContext.Database.ExecuteSqlRaw("UPDATE LocInvType_View SET Active=0 WHERE Id=@Id", param);
            return true;
        }

        /// <summary>
        /// Creates a link between a Location and an InvoiceType OR marks an already existing one as Active
        /// </summary>
        /// <param name="link">The link that should be created</param>
        /// <returns>True if succeeded, False if either the location or the InvoiceType wasn't found OR if it exists and is active</returns>
        public bool AddOneLink(LocationInvoiceTypeRelationDto link)
        {
            var locationFromDb = DbContext.LocationsViews.Where(l => l.Description == link.LocationDescription).SingleOrDefault();
            if (locationFromDb is null)
            {
                return false;
            }
            var invoiceTypeFromDb = DbContext.InvoiceTypesViews.Where(it => it.Name == link.InvoiceTypeName).SingleOrDefault();
            if (invoiceTypeFromDb is null)
            {
                return false;
            }

            var elemToAdd = new LocInvTypeView()
            {
                Id = 0,
                Location = locationFromDb.Id,
                InvoiceType = invoiceTypeFromDb.Id,
                Active = true
            };

            var alreadyInDb = DbContext.LocInvTypeViews.Where(lit => lit.Location == elemToAdd.Location && lit.InvoiceType == elemToAdd.InvoiceType).SingleOrDefault();
            if (alreadyInDb is not null)
            {
                if (alreadyInDb.Active)
                {
                    return false; // already exists and it is active
                }
                else
                {
                    // already exists but it is marked as inactive -> mark it back as Active

                    var param = new SqlParameter("@Id", alreadyInDb.Id);
                    DbContext.Database.ExecuteSqlRaw("UPDATE LocInvType_View SET Active=1 WHERE Id=@Id", param);
                    return true;
                }
            }
            else
            {
                // link does not exist in db -> it needs to be created and marked as active
                var param1 = new SqlParameter("@Location", elemToAdd.Location);
                var param2 = new SqlParameter("@InvoiceType", elemToAdd.InvoiceType);

                DbContext.Database.ExecuteSqlRaw("INSERT INTO LocInvType_View(Location, Invoice_Type, Active) VALUES (@Location, @InvoiceType, 1)", param1, param2);
                var newElemFromDb = DbContext.LocInvTypeViews.Where(lit => lit.InvoiceType == elemToAdd.InvoiceType && lit.Location == elemToAdd.Location && lit.Active == true).SingleOrDefault();
                if (newElemFromDb is null)
                {
                    return false; // something went wrong
                }
                elemToAdd.Id = newElemFromDb.Id;
                if (!createCounterForLink(elemToAdd, invoiceTypeFromDb)) // create a counter for the newly created link
                {
                    revertChanges(elemToAdd); // counter type wasn't found -> counter couldn't be created -> changes should be reverted
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Creates a counter for a Location - InvoiceType relation
        /// </summary>
        /// <param name="link">The relation</param>
        /// <param name="invoiceType">The Invoice Type</param>
        /// <returns>True if succeeded, False if the Counter Type wasn't found</returns>
        private bool createCounterForLink(LocInvTypeView link, InvoiceTypesView invoiceType)
        {
            var counterType = DbContext.CounterTypesViews.Where(ct => ct.Id == invoiceType.CounterType).SingleOrDefault();
            if (counterType is null)
            {
                return false;
            }

            var counterToAdd = new CountersView()
            {
                Id = 0,
                CounterType = counterType.Id,
                Location = link.Location,
                SerialNr = String.Empty
            };

            var param1 = new SqlParameter("@CounterType", counterToAdd.CounterType);
            var param2 = new SqlParameter("@Location", counterToAdd.Location);
            var param3 = new SqlParameter("@SerialNr", counterToAdd.SerialNr);

            DbContext.Database.ExecuteSqlRaw("INSERT INTO Counters_View(Counter_Type, Location, Serial_Nr) VALUES (@CounterType, @Location, @SerialNr)", param1, param2, param3);
            return true;
        }

        /// <summary>
        /// Reverts the addition of the new link
        /// </summary>
        /// <param name="elemToDelete">The new link</param>
        private void revertChanges(LocInvTypeView elemToDelete)
        {
            var param = new SqlParameter("@Id", elemToDelete.Id);

            DbContext.Database.ExecuteSqlRaw("DELETE FROM Counters_View WHERE Id=@Id", param);
        }

        #endregion
    }
}

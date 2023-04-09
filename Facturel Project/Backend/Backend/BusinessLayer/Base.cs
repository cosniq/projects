using RepositoryLayer;

namespace BusinessLayer
{
    public class Base
    {
        #region Fields
        protected CustomMapper mapper = new CustomMapper();
        protected TIAProjContext DbContext;
        #endregion

        #region Constructors
        public Base(TIAProjContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
    }
}

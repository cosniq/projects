using DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace BusinessLayer
{
    public class Users : Base
    {
        #region Contructors
        public Users(TIAProjContext dbContext) : base(dbContext)
        {
        }
        #endregion
        
        #region Public Functions

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="credentials">The users username and password</param>
        /// <returns>Null if user not found -> a dto otherwise. The token should be calculated by the caller!!!</returns>
        public UserWithTokenDto? Login(CredentialsDto credentials)
        {
            string encodedPass;
            if (credentials.Password is not null)
            {
                encodedPass = EncodePasswordToBase64(credentials.Password);
            }
            else
            {
                return null;
            }
            var userFromDB = DbContext.UsersViews.Where(u => u.Username == credentials.Username && u.Password == encodedPass).SingleOrDefault();
            if (userFromDB is not null)
            {
                return mapper.Map(userFromDB);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the details of a user from the database
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns>The details of the user</returns>
        public UsersWithDetailsView? GetDetailsOfUser(int userId)
        {
            var userFromDB = DbContext.UsersWithDetailsViews.Where(u => u.Id == userId).SingleOrDefault();
            if (userFromDB is not null)
            {
                return userFromDB;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user">A new User with every detail</param>
        /// <returns>True if succeeded, False if username is already taken</returns>
        public bool RegisterNewUser(RegisterUserDto user)
        {
            var userFromDb = DbContext.UsersViews.Where(u => u.Username == user.Username).SingleOrDefault();
            if (userFromDb is not null)
            {
                return false;
            }
            else
            { 
                var param1 = new SqlParameter("@Username", user.Username);
                var param2 = new SqlParameter("@Password", EncodePasswordToBase64(user.Password));
                var param3 = new SqlParameter("@Email", user.Email);
                var param4 = new SqlParameter("@DateOfBirth", user.DateOfBirth);
                DbContext.Database.ExecuteSqlRaw("EXEC sp_register_new_user @Username, @Password, @Email, @DateOfBirth", param1, param2, param3, param4);
                return true;
            }
        }

        /// <summary>
        /// Updates a users details
        /// </summary>
        /// <param name="newUser">An object with the new details but same id</param>
        /// <returns>True if succeeded, False if the user wasn't found</returns>
        public bool UpdateUserDetails(UsersWithDetailsView newUser)
        {
            var userFromDb = DbContext.UsersWithDetailsViews.Where(u => u.Id == newUser.Id).SingleOrDefault();
            if (userFromDb is null)
            {
                return false;
            }
            else
            {
                var param1 = new SqlParameter("@Id", newUser.Id);
                var param2 = new SqlParameter("@Username", newUser.Username);
                var param3 = new SqlParameter("@Email", newUser.Email);
                var param4 = new SqlParameter("@DateOfBirth", newUser.DateOfBirth);

                DbContext.Database.ExecuteSqlRaw("EXEC UpdateUsersWithDetails @Id, @Username, @Email, @DateOfBirth", param1, param2, param3, param4);
                return true;
            }
        }
        #endregion

        #region Private Functions
        private static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch
            {
                throw new Exception("Error when trying to encode the password");
            }
        }
        #endregion
    }
}

using DataLibrary.Data.DataAccess;
using Library.Data.Services;

namespace DataLibrary.Data.Services
{
    public static class ServiceFactory
    {
        public static IUserService GetUserService()
        {
            return new UserService(new SqlDataAccess());
        }

        public static IBookService GetBookService()
        {
            return new BookService(new SqlDataAccess());
        }
    }
}

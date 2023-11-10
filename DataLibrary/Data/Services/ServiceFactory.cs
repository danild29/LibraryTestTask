using DataLibrary.Data.DataAccess;
using Library.Data.Services;

namespace DataLibrary.Data.Services
{
    public static class ServiceFactory
    {
        public static UserService GetUserService()
        {
            return new UserService(new SqlDataAccess());
        }

        public static BookService GetBookService()
        {
            return new BookService(new SqlDataAccess());
        }
    }
}

using DataLibrary.Data.DataAccess;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Data.Services
{
    public class UserService
    {
        private readonly ISqlDataAccess _data;


        public UserService(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task InsertUser(User user)
        {
            string sql = @"INSERT INTO [Users](FirstName, SurName, Email) values(@FirstName, @SurName, @Email)";
            await _data.SaveData(sql, new { user.FirstName, user.SurName, user.Email, });
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            string sql = @"SELECT * from [Users]";
            return await _data.LoadData<User, dynamic>(sql, new { });
        }


        public async Task<User> GetUser(int userId)
        {
            string sql = @"SELECT *
                        from [Users] as u
                        left join [Loans] as l on u.Id = l.MemberId
                        left join [Books] as b on l.BookId = b.Id
                        Where c.Id  = @Id
                        ";
            var res = await _data.LoadDataWithFilledList<Loan, dynamic>(sql, new { Id = userId });

            string sql1 = @"SELECT * from [Users] Where Id = @Id";
            var user = (await _data.LoadData<User, dynamic>(sql1, new { Id = userId })).First();
            user.Loans = res.ToList();
            return user;
        }
    }
}
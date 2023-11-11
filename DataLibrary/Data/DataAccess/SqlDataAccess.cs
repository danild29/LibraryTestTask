using Dapper;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace DataLibrary.Data.DataAccess
{

    public class SqlDataAccess : ISqlDataAccess
    {
        public static string GetConnectionString(string name = "LibraryDB")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parametrs)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {

                return await connection.QueryAsync<T>(sql, parametrs);
            }

        }


        public async Task SaveData<T>(string sql, T parametrs)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                await connection.ExecuteAsync(sql, parametrs);

            }

        }


        public async Task<IEnumerable<Loan>> GetLoansWithBook<T, U>(string sql, U parametrs, string split)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                var res = connection.QueryAsync<Loan, Book, Loan>(sql, (loan, book) =>
                {
                    loan.Book = book;
                    return loan;
                },
                   parametrs, 
                   splitOn: split
                );

                return await res;
            }
        }



        public async Task<IEnumerable<Loan>> GetLoan<U>(string sql, U parametrs, string split)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                IEnumerable<Loan> res = await connection.QueryAsync<Loan, User, Book, Loan>(sql, (loan, user, book) =>
                {
                    if (loan != null)
                    {
                        loan.User = user;
                        loan.Book = book;
                    }
                    return loan;
                },
                   parametrs,
                   splitOn: split 
                );

                return  res;
            }
        }
    }
}

using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string sql, U parametrs);
    Task SaveData<T>(string sql, T parametrs);
    Task<IEnumerable<Loan>> GetLoansWithBook<T, U>(string sql, U parametrs, string split);
    Task<IEnumerable<Loan>> GetLoan<U>(string sql, U parametrs, string split);
}
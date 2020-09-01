using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Questionnaire.Data.Models;

namespace Questionnaire.Data
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected async Task<T> GetData<T>(Func<IDbConnection, Task<T>> func)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return await func(connection);
        }
    }
}

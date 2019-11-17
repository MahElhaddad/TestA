using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Transactions.Model;

namespace Transactions.Query
{
    public class QueryService : IQueryService
    {
        private readonly string _connectionString;

        public QueryService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("message", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Transaction>> GetAllTran()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                return await conn.QueryAsync<Transaction>("SELECT FromUserId, ToUserId, Amount FROM dbo.Transactions;");
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTranById(string fromUserId)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.QueryAsync<Transaction>("SELECT FromUserId, ToUserId, Amount FROM dbo.Transactions where FromUserId = " + fromUserId + ";");
            }
        }
    }
}

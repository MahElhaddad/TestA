using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions.Model;

namespace Transactions.Query
{
    public interface IQueryService
    {
        Task<IEnumerable<Transaction>> GetAllTran();
        Task<IEnumerable<Transaction>> GetAllTranById(string fromUserId);
    }
}

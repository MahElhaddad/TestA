using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions.Model;

namespace Transactions.Manipulation
{
    public interface IManipService
    {
        Task<Model.Transaction> SaveTrans(string fromUserId, string toUserId, double Amount);
    }
}

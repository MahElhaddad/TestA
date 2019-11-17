using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transactions.Model;

namespace Transactions.Manipulation
{
    public class ManipService : IManipService
    {
        private readonly DBTransContext _context;

        public ManipService(DBTransContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Model.Transaction> SaveTrans(string fromUserId, string toUserId, double amount)
        {
            var newTrans = new Model.Transaction() { FromUserId = fromUserId, ToUserId = toUserId, Amount = amount };

            await _context.Transactions.AddAsync(newTrans);

            await _context.SaveChangesAsync();

            return newTrans;
        }
    }
}

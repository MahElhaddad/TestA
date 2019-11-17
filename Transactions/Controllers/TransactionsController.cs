using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transactions.Query;
using Transactions.Manipulation;
using Transactions.Model;

namespace Transactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IQueryService _query;
        private readonly IManipService _mainp;

        public TransactionsController(IQueryService query, IManipService mainp)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _mainp = mainp ?? throw new ArgumentNullException(nameof(mainp));
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            return (await _query.GetAllTran()).ToList();
        }

        // GET api/values
        [HttpGet("{fromUseerId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetById(string fromUseerId)
        {
            return (await _query.GetAllTranById(fromUseerId)).ToList();
        }

        // POST api/values
        [HttpPost]
        public void SaveTrans([FromBody] Model.Transaction value)
        {
            _mainp.SaveTrans(value.FromUserId, value.ToUserId, value.Amount);
        }
    }
}

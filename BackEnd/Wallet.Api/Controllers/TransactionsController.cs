using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Business;
using Wallet.Application.Dtos.Transanction;

namespace Wallet.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController(TransactionBusiness _transactionBusiness) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFilteredTransactionsAsync([FromQuery] FilterTransaction filter)
        {
            try
            {
                var result = await _transactionBusiness.GetFilteredTransactionsAsync(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

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
        [Route("received")]
        public async Task<IActionResult> GetAllRecipientTransactionsAsync([FromQuery] FilterTransaction filter)
        {
            try
            {
                var result = await _transactionBusiness.GetAllRecipientTransactionsAsync(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> GetAllSentTransactionsAsync([FromQuery] FilterTransaction filter)
        {
            try
            {
                var result = await _transactionBusiness.GetAllSentTransactionsAsync(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}

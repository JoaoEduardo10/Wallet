using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Business;
using Wallet.Application.Dtos.Wallet;
using Wallet.Application.Utils;

namespace Wallet.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController(WalletBusiness _walletBusiness) : ControllerBase
    {

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetWalletByUserIdAsync(Guid userId)
        {
            try
            {
                var result = await _walletBusiness.GetWalletByUserIdAsync(userId);

                if (!result.Success)
                {
                    return BadRequest(HandlingErrors.FormateErrors(result));
                }

                return Ok(result.Value);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> TransferAmountAsync([FromBody] TransferAmountDto transfer)
        {
            try
            {
                var result = await _walletBusiness.TransferAmountAsync(transfer);

                if (!result.Success)
                {
                    return BadRequest(HandlingErrors.FormateErrors(result));
                }

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("add-balance/{id}")]
        public async Task<IActionResult> AddBalanceAsync( Guid id, [FromBody] decimal amount)
        {
            try
            {
                var result = await _walletBusiness.AddBalanceAsync(id, amount);

                if (!result.Success)
                {
                    return BadRequest(HandlingErrors.FormateErrors(result));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

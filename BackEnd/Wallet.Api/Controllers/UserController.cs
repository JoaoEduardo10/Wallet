using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Business;
using Wallet.Application.Dtos.User;
using Wallet.Application.Utils;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserBusiness _userBusiness) : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                var result = await _userBusiness.CreateUserAsync(user);

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
    }
}

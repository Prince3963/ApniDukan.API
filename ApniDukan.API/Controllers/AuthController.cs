using ApniDukan.Application.IServices;
using ApniDukan.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ApniDukan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register/shop-owner")]
        public async Task<IActionResult> RegisterShopOwner([FromBody] ShopOwnerRegisterRequest registerDto, CancellationToken cancellationToken)
        {
            var result = await authService.RegisterShopOwners(registerDto, cancellationToken);
            if (result.Success != true)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

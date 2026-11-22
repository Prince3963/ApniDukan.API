using ApniDukan.Application.ServiceResponse;
using ApniDukan.Domain.DTOs.Auth;

namespace ApniDukan.Application.IServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<ShopOwnerRegisterResponse>> RegisterShopOwners(ShopOwnerRegisterRequest request, CancellationToken cancellationToken = default);
    }
}

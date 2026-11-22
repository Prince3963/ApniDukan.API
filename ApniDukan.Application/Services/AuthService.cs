using ApniDukan.Application.IRepos;
using ApniDukan.Application.IServices;
using ApniDukan.Application.RepoInterface;
using ApniDukan.Application.ServiceResponse;
using ApniDukan.Domain.DTOs.Auth;
using ApniDukan.Domain.ENUMs;
using ApniDukan.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ApniDukan.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepo<Shop> shopOwnerRepo;
        private readonly IGenericRepo<User> userRepo;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<ShopOwnerRegisterRequest> validator;
        public AuthService(IGenericRepo<Shop> shopOwnerRepo, IGenericRepo<User> userRepo, IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork, IValidator<ShopOwnerRegisterRequest> validator)
        {
            this.shopOwnerRepo = shopOwnerRepo;
            this.passwordHasher = passwordHasher;
            this.userRepo = userRepo;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
        public async Task<ServiceResponse<ShopOwnerRegisterResponse>> RegisterShopOwners(ShopOwnerRegisterRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // 1. Validate registration request
                var validationResult = await validator.ValidateAsync(
                    request,
                    cancellationToken
                );

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(
                        " | ",
                        validationResult.Errors.Select(e => e.ErrorMessage)
                    );

                    return ServiceResponse<ShopOwnerRegisterResponse>
                        .FailureResponse(errors);
                }

                // 1. Check whether email already exists
                var emailExist = await userRepo.AnyAsync(u => u.Email == request.Email, cancellationToken);
                if (emailExist)
                {
                    return ServiceResponse<ShopOwnerRegisterResponse>.FailureResponse("Email is already registered.");
                }

                // 2. Create User entity
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    AlternativeNumber = request.AlternativeNumber,
                    PermanentAddress = request.PermanentAddress,
                    CurrentAddress = request.CurrentAddress,

                    Role = UserRole.ShopOwner,
                    IsActive = true
                };

                // 3. Hash password
                user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

                // 4. Save User and Shop inside transaction
                Shop? createdShop = null;

                await unitOfWork.ExecuteInTransactionAsync(async ct =>
                {
                    // Create User
                    var createdUser = await userRepo.AddAsync(user, ct);

                    //Create Shop
                    var shop = new Shop
                    {
                        Name = request.ShopName,
                        Description = request.ShopDescription,
                        ImageUrl = request.ShopImageUrl,
                        Address = request.ShopAddress,
                        PhoneNumber = request.PhoneNumber,


                        ShopOwnerId = createdUser.Id,
                        IsOpen = true,
                        IsActive = true
                    };

                    createdShop = await shopOwnerRepo.AddAsync(shop, ct);
                }, cancellationToken);

                // 5. Response DTO
                var responseData = new ShopOwnerRegisterResponse
                {
                    UserId = user.Id,
                    ShopId = createdShop.Id,
                    OwnerName = user.Name,
                    ShopName = createdShop.Name,
                };

                // 6. Return success response
                return ServiceResponse<ShopOwnerRegisterResponse>.SuccessResponse(responseData, "Shop owner registered successfully.");
            }
            catch (Exception)
            {
                return ServiceResponse<ShopOwnerRegisterResponse>
            .FailureResponse(
                "Something went wrong while registering the shop owner."
            );
            }
        }
    }
}

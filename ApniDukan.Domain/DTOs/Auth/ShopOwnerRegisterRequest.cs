using ApniDukan.Domain.ENUMs;
using ApniDukan.Domain.Models;

namespace ApniDukan.Domain.DTOs.Auth
{
    public class ShopOwnerRegisterRequest
    {
        //User Information
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? AlternativeNumber { get; set; }
        public string? PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }

        //Shop Information
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string? ShopImageUrl { get; set; }
        public string ShopAddress { get; set; }

    }
}

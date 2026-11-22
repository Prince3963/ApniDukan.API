using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class User : GenericEntity
    {
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string? AlternativeNumber { get; set; }
        public string? RoomMatesNumber { get; set; }
        public string? RoomMatesAlternativeNumber { get; set; }
        public string? RoomOwnerNumber { get; set; }
        public string? PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string? ProfileImage { get; set; }
        public DocumentsType Document  { get; set; }
        public string? DocumentImage { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Shop> Shops { get; set; } = new List<Shop>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

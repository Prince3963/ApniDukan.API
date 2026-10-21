namespace ApniDukan.Domain.Models
{
    public class User : GenericEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string? RoomMatesNumber { get; set; }
        public string? RoomMatesAlternativeNumber { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string ProfileImage { get; set; }
        public string DocumentImage  { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

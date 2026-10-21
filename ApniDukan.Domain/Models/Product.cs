using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class Product : GenericEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public UnitType Unit { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;

        //Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

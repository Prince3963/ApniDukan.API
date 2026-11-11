using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class Product : GenericEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public UnitType Unit { get; set; }
        public string? ImageUrl { get; set; } 
        public bool IsActive { get; set; } = true;

        //Foreign Key
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        //Navigation property
        public User? User { get; set; }
        public Category? Categories{ get; set; } 
        public ICollection<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();
    }
}

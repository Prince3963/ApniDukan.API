namespace ApniDukan.Domain.Models
{
    public class Category : GenericEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        // Foreign Key
        public int ShopId { get; set; }

        // Navigation Properties
        public Shop? Shops { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

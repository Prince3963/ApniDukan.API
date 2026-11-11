namespace ApniDukan.Domain.Models
{
    public class Category : GenericEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Product { get; set; } = new List<Product>();
    }
}

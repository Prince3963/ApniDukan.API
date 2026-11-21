using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class Order : GenericEntity
    {
        public Decimal TotalAmount { get; set; }
        public StatusType Status { get; set; }

        //Foreing Key
        public int UserID { get; set; }
        public int ShopId { get; set; }


        //Navigation property
        public User? Customers { get; set; }
        public Shop? Shops { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}

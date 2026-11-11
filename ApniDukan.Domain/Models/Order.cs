using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class Order : GenericEntity
    {
        public Decimal TotalAmount { get; set; }
        public StatusType Status { get; set; }

        //Foreing Key
        public int UserID { get; set; }

        //Navigation property
        public User? Users { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}

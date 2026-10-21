using ApniDukan.Domain.ENUMs;

namespace ApniDukan.Domain.Models
{
    public class Order : GenericEntity
    {
        public Decimal TotalAmount { get; set; }
        public StatusType Status { get; set; }
    }
}

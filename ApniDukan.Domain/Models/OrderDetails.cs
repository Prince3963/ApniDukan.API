namespace ApniDukan.Domain.Models
{
    public class OrderDetails : GenericEntity
    {
        public Decimal Discount { get; set; }
        public Decimal BillAmount { get; set; }
    }
}

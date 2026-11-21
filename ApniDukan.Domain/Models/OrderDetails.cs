namespace ApniDukan.Domain.Models
{
    public class OrderDetails : GenericEntity
    {
        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal BillAmount { get; set; }

        //Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        //Navigation Property
        public Order? Orders { get; set; }
        public Product? Products { get; set; }
    }
}

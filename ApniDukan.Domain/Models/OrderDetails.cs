namespace ApniDukan.Domain.Models
{
    public class OrderDetails : GenericEntity
    {
        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal discount { get; set; }
        public decimal BillAmount { get; set; }

        //Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        //Navigation Property
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}

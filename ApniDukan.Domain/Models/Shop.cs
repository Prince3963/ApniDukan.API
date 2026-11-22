using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApniDukan.Domain.Models
{
    public class Shop : GenericEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsOpen { get; set; } = true;

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign Key
        public int ShopOwnerId { get; set; }

        // Navigation Properties
        public User? ShopOwner { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();

        public ICollection<Category> Categories { get; set; }
            = new List<Category>();

        public ICollection<Order> Orders { get; set; }
            = new List<Order>();
    }
}

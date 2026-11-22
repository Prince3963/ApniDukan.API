using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApniDukan.Domain.DTOs.Auth
{
    public class ShopOwnerRegisterResponse
    {
        public int UserId { get; set; }
        public int ShopId { get; set; }

        public string OwnerName { get; set; }
        public string ShopName { get; set; }
    }
}

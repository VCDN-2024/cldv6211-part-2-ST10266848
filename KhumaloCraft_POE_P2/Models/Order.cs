using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft_POE_P2.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreditCardName { get; set; } = null!;
        public string CreditCardNumber { get; set; } = null!;
        public string CreditCardExpDate { get; set; } = null!;
        public string CreditCardCCVcode { get; set; } = null!;
        public string ShippingAddress { get; set; } = null!;

        // FK:
        public Product? Product { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}

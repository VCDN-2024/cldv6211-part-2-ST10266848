using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace KhumaloCraft_POE_P2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string UserId { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public bool ProductAvailability { get; set; }
        public double ProductPrice { get; set; }
        public string ImageUrl { get; set; } = null!;

        // FK:
        public IdentityUser User { get; set; } = null!;

    }
}

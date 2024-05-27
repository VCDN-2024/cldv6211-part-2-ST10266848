using KhumaloCraft_POE_P2.Models;

namespace KhumaloCraft_POE_P2.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<Order>? Orders { get; set; }
        public string? FilterProductName { get; set; }
        public string? FilterUserName { get; set; }
        public DateTime? FilterOrderDate { get; set; }
        
    }
}

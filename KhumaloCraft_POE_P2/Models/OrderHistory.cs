namespace KhumaloCraft_POE_P2.Models
{
    public class OrderHistory
    {
        public int OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public DateTime ChangeDate { get; set; }
        public Order? Order { get; set; }
    }
}

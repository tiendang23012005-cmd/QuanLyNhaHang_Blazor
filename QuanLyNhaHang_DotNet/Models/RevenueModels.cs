namespace RestaurantManagement.Frontend.Models
{
    public class RevenueSummaryDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalBills { get; set; }
        public int TotalCustomers { get; set; }
        public decimal MonthlyTarget { get; set; } = 100000000; // 100 triệu đ
        public List<MonthlyRevenueItem> MonthlyRevenue { get; set; } = new();
        public List<RevenueDetailItem> RevenueDetails { get; set; } = new();
    }

    public class MonthlyRevenueItem
    {
        public string Month { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
    }

    public class RevenueDetailItem
    {
        public string Month { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int Bills { get; set; }
        public int Customers { get; set; }
    }
}
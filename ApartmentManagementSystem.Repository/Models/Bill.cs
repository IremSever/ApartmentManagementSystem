namespace ApartmentManagementSystem.Repository.Models
{
    public class Bill
    {
        public int BillId { get; set; } = default!;
        public string Type { get; set; } = default!; // Electricity, Water, Natural Gas
        public decimal Amount { get; set; } = default!;
        public DateTime MonthYear { get; set; } = default!; // Year and month information for the bill
    }

}

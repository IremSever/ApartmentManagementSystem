namespace ApartmentManagementSystem.Repository.Models
{
    public class Payment
    {
        public int PaymentId { get; set; } = default!;
        public decimal Amount { get; set; } = default!;
        public DateTime PaymentDate { get; set; } = default!;
        public string PaymentType { get; set; } = default!; // Dues/Bill (Electricity-Water-Natural Gas)
        public Guid ResidentId { get; set; } 
        public int ApartmentId { get; set; } 
        public virtual AppUser Resident { get; set; } = default!;
        public virtual AppUser Apartments { get; set; } = default!;
    }

}

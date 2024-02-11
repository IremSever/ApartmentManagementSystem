namespace ApartmentManagementSystem.Repository.Models
{
     public class Apartment
     {
         public int ApartmentId { get; set; } = default!;
         public string BlockInfo { get; set; } = default!;
         public string Status { get; set; } = default!; // Occupied-vacant
         public string Type { get; set; } = default!; // 2+1,3+1... 
         public int Floor { get; set; } = default!;
         public int ApartmentNumber { get; set; } = default!;
         public virtual AppUser? Resident { get; set; }
            
    }
}

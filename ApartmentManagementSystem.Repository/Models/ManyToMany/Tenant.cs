namespace ApartmentManagementSystem.Repository.Models.ManyToMany
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<ResidentOwner> ResidentOwners { get; } = default!;
    }
}

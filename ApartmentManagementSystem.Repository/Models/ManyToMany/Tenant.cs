namespace ApartmentManagementSystem.Repository.Models.ManyToMany
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<ResidentOwner> ResidentOwners { get; set; } = new List<ResidentOwner>();
    }
}

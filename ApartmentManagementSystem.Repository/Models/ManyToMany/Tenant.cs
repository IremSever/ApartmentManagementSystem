namespace ApartmentManagementSystem.Repository.Models.ManyToMany
{
    class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<ResidentOwner>? ResidentOwner { get; set; }
    }
}

namespace ApartmentManagementSystem.Service.Event
{
    public class UserCreatedEvent
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}

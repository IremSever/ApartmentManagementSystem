namespace ApartmentManagementSystem.Service.DTOs
{
    public class UserCreateRequestDto
    {
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool IsResidentOwner { get; set; }
    }
}

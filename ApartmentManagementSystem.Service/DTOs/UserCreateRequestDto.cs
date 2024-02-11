public class UserCreateRequestDto
{
    public string UserName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool IsResidentOwner { get; set; }
    public string FullName { get; set; } = default!;
    public string IdentityNumber { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
}

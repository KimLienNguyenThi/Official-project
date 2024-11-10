namespace WebAPI.DTOs
{
    public class UserAuthentication
    {
        public string Sdt { get; set; } = null!;

        public string? PasswordDg { get; set; }

        public string? HoTen { get; set; }

        public string? Email { get; set; }
    }
}

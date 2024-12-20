namespace WebAPI.Responses
{
    public class LoginResponseModel
    {
        public string? SDT {  get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

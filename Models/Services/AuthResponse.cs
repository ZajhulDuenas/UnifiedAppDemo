
namespace Models.Services
{
    public class AuthResponse
    {
        public bool isOk { get; set; } = false;
        public string token { get; set; } = string.Empty;
    }
}

using Microsoft.IdentityModel.Tokens;

namespace Services.B2B_Identity.Configurations
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int Expiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}

namespace Application.Dtos
{
    public class JwtSettingOptions
    {
        public const string Section = "Jwt";

        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public TimeSpan TokenLifetime { get; set; }
    }
}

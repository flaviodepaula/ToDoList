namespace Domain.Support
{
    public record JwtConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int ExpirationMinutes { get; set; } = 60;
    }
}

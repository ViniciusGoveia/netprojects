namespace Blog;

public static class Configuration
{
    //Token - JWT - Json Web Token
    public static string? JwtKey { get; set; } = "cb2ca536-dcce-4401-8d31-eb1995dcba0a";
    public static string? ApiKeyName;
    public static string? ApiKey;
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

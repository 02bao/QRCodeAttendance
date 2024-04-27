namespace QRCodeAttendance.QRCodeAttendance.Infrastructure.Constant;

public class ConfigKey
{
    private readonly IConfiguration _configuration;
    public static string AT_COOKIES { get; set; } = "";
    public static string RT_COOKIES { get; set; } = "";
    public static string JWT_KEY { get; set; } = "";
    public static string VALID_AUDIENCE { get; set; } = "";
    public static string VALID_ISSUER { get; set; } = "";

    public ConfigKey(IConfiguration configuration)
    {
        _configuration = configuration;
        AT_COOKIES = _configuration["JWT:AccessTokenCookies"];
        RT_COOKIES = _configuration["JWT:RefreshTokenCookies"];
        JWT_KEY = _configuration["JWT:Key"];
        VALID_AUDIENCE = _configuration["JWT:ValidAudience"];
        VALID_ISSUER = _configuration["JWT:ValidIssuer"];
    }
    public static DateTime getATExpiredTime()
    {
        return DateTime.UtcNow.AddMinutes(10);
    }

    public static DateTime getRTExpiredTime()
    {
        return DateTime.UtcNow.AddHours(1);
    }
}

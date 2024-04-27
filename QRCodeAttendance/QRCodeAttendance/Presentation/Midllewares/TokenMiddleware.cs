using QRCodeAttendance.Application.Token;

namespace QRCodeAttendance.Presentation.Midllewares;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        ITokenService? service = context.RequestServices.GetService<ITokenService>();
        if (service == null)
        {
            throw new Exception("Token service not found");
        }
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            TokenDecodedDTO info = service.DecodeToken(token);
            if (!string.IsNullOrEmpty(info.Email))
            {
                context.Items["Id"] = info.Id;
                context.Items["Email"] = info.Email;
                context.Items["Role"] = info.Role;
            }
        }
        await _next(context);
    }
}

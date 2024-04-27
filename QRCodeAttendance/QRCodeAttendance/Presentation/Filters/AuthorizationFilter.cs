using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QRCodeAttendance.QRCodeAttendance.Presentation.Filters;

public class RoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<string> _roles = new();
    public RoleAttribute(params string[] roles)
    {
        _roles = new List<string>(roles);
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            string user = context.HttpContext.Items["Email"] as string ?? "";
            if (string.IsNullOrEmpty(user))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            string role = context.HttpContext.Items["Role"] as string ?? "";
            if (string.IsNullOrEmpty(role))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (!_roles.Contains(role))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
        catch (Exception ex)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
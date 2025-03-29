using System.Text;

namespace UserManagementAPI.Auth;
public class BasicAuth
{
    private readonly RequestDelegate _next;

    public BasicAuth(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Split(" ")[1]));
        var parts = credentials.Split(':');
        var username = parts[0];
        var password = parts[1];

        if (username == "admin" && password == "docosoft")
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }
}

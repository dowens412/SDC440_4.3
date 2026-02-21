using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace David_Owens_GP_4._3.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // If there is no Authorization header, deny the request
            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Expected format: "Basic <base64-encoded-credentials>"
            var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
            var authHeaderParts = authHeader.Split(' ');

            if (authHeaderParts.Length != 2 || authHeaderParts[0] != "Basic")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Decode base64 -> "username:password"
            string credentials;
            try
            {
                credentials = System.Text.Encoding.UTF8.GetString(
                    System.Convert.FromBase64String(authHeaderParts[1])
                );
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var parts = credentials.Split(':');

            // Validate username/password
            if (parts.Length != 2 || parts[0].ToLower() != "instructor01" || parts[1] != "Password01")
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
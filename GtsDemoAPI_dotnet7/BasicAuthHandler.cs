using GtsDemoAPI_dotnet7.Models;
using System.Text;

namespace GtsDemoAPI_dotnet7
{
    public class BasicAuthHandler
    {
        private readonly RequestDelegate next;
        private readonly string relm;

        public BasicAuthHandler(RequestDelegate next, string relm)
        { 
            this.next = next;
            this.relm = relm;
        }

        private readonly ApiDemoDbContext _apiDemoDbContext;
        public BasicAuthHandler(ApiDemoDbContext apiDemoDbContext)
        {
            _apiDemoDbContext = apiDemoDbContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var header = context.Request.Headers["Authorization"].ToString();
            var encodedCreds = header.Substring(6);
            var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCreds));
            string[] uidpwd = creds.Split(':');
            var uid = uidpwd[0];
            var password = uidpwd[1];

            var memberResult = _apiDemoDbContext.members.FirstOrDefault(u => u.ID == Int32.Parse(uid));

            // Authorization works, however Authorizate button does not show in API
            if (memberResult == null) 
            {
                await context.Response.WriteAsync("Null Author");
                return;
            }

            else if (Int32.Parse(uid) != memberResult.ID)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await next(context);
        }
    }
}

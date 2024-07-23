
namespace WebAPI.Middleware
{
    public class Custommiddleware : IMiddleware
    {
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

             Console.WriteLine("hello from my custom middleware- incoming resp");
            await next.Invoke(context);
            Console.WriteLine("hello from my custom middleware- outgoing resp");
        }
    }
    public static class CustommiddlewareExtension
    {
        public static IApplicationBuilder MycustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Custommiddleware>();
        }
    }

}

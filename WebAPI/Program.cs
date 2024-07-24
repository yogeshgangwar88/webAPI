
using Dbcontext;
using Interfaces;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));
            builder.Services.AddMvc(services => services.EnableEndpointRouting = false).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IUsers, UserRepo>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Add your client-side application's origin here
                               .WithMethods("GET", "POST", "PUT", "DELETE")
                               .AllowAnyHeader();
                    });
            });
            
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<Custommiddleware>();
            
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            //else
            //    app.UseExceptionHandler();
            app.UseHsts();
            app.UseHttpsRedirection();
            if (!Directory.Exists(Path.Combine(builder.Environment.ContentRootPath, "UserImages")))
            {
                Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "UserImages"));
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "UserImages")),
                RequestPath = "/staticImages"

            });
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
            ///custom middleware ///
            app.MycustomMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}


using  Dbcontext;
using  Interfaces;
using  Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));
            builder.Services.AddMvc().AddNewtonsoftJson(options =>
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
                               .WithMethods("GET","POST","PUT","DELETE")
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
          
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "UserImages")),
                RequestPath = "/staticImages"

            });
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

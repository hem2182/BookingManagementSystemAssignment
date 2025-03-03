using BookingManagementDataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;

namespace BookingManagementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Logging
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            // Register EF Core DbContext (Assuming it's inside the Repository Project)
            builder.Services.AddDbContext<BookingSystemDbContext>();

            // Add services to the container.

            // Register Repository from the separate project
            builder.Services.AddScoped<IBookingSystemDb, BookingSystemDb>();

            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("Policy1", policy =>
                //{
                //    policy.WithOrigins("http://example.com", "http://www.contoso.com");
                //});

                options.AddPolicy("CorsPolicyReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:52540").AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking Management API", Version = "v1" });

                options.OperationFilter<FileUploadOperationFilter>();
            });

            // Allow large file uploads (optional)
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
            });

            var app = builder.Build();

            // Ensure WebRootPath is set
            var env = app.Services.GetRequiredService<IWebHostEnvironment>();
            if (string.IsNullOrEmpty(env.WebRootPath))
            {
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                Directory.CreateDirectory(env.WebRootPath); // Ensure it exists
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Management API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
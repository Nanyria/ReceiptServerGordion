using Microsoft.EntityFrameworkCore;
using ReceiptServer.Data;
using ReceiptServer.Repositories;
using ReceiptServer.Services;
using System.Text.Json.Serialization;

namespace ReceiptServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    // Ignore cycles to prevent the "possible object cycle" exception
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    // Optionally increase max depth if you need deeper graphs
                    opt.JsonSerializerOptions.MaxDepth = 64;
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS (policy named "AllowAll" as an example)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            builder.Services.AddScoped<IReceiptService, ReceiptService>();
            builder.Services.AddScoped<IReceiptRepositoriy, ReceiptRepository>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                //try
                //{
                //    dbContext.Database.Migrate();
                //}
                //catch (Exception ex)
                //{
                //    logger.LogError(ex, "Database migration failed.");
                //    throw;
                //}
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Apply CORS policy before routing/authorization
            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

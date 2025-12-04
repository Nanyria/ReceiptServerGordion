using Microsoft.EntityFrameworkCore;
using ReceiptServer.Data;
using ReceiptServer.Models;
using ReceiptServer.Repositories;
using ReceiptServer.Services;
using RecieptServer;
using RecieptServer.Models;
using RecieptServer.Services;
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
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

            // for the concrete Receipt service
            builder.Services.AddScoped<IReceiptServerService<ReceiptDTO>, ReceiptService>();
            builder.Services.AddScoped<IReceiptServerService<ArticleDTO>, ArticleService>();
			
            builder.Services.AddScoped<IReceiptServiceRepositoriy<Receipt>, ReceiptRepository>();
            builder.Services.AddScoped<IReceiptServiceRepositoriy<Article>, ArticleRepository>();
			builder.Services.AddAutoMapper(typeof(MappingConfig));


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDbContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();

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

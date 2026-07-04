
using Dsw2026Ej15.Api.Configurations;
using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddApplicationPersistence(builder.Configuration);


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersistance, PersistenceEf>();
            builder.Services.AddHealthChecks();
            


            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHealthChecks("/healthy :)");

            app.LoadSpecialityData();
            app.Run();
        }
    }
}

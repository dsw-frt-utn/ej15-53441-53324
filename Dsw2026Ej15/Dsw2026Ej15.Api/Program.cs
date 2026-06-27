
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

            var connectionString = "Data Source = (localdb)\\MSSQLLocalDB;DataBase=Dsw2026Ej15; Integrated security=true;Connect Timeout=30;Encrypt=true;TrustServerCertificate=true;";

            // Add services to the container.
            builder.Services.AddDbContext<Dsw2026Ej15DbContext>(Options =>
            {
                Options.UseSqlServer(connectionString);
            });



            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IPersistance, PersistenceInMemory>();
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

            var persistence = app.Services.GetRequiredService<IPersistance>();
            await persistence.LoadSpecialities();
            app.Run();
        }
    }
}

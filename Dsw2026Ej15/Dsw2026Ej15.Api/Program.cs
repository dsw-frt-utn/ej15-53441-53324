using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedicalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersistance, PersistenceEf>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 
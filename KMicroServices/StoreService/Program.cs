using Microsoft.EntityFrameworkCore;
using StoreService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* Database context independency injection*/
var db_host = Environment.GetEnvironmentVariable("DB_HOST");
var db_name = Environment.GetEnvironmentVariable("DB_NAME");
var db_password = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connString = $"Data Source={db_host};Initial Catalog={db_name};User ID=sa;Password={db_password};TrustServerCertificate=true";

builder.Services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(connString));

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

app.UseAuthorization();

app.MapControllers();

app.Run();

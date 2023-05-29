using CustomerService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* DbContext dependency injection*/
//var db_host = "MDI1221459";
//var db_name = "db_product";
//var db_password = "12345";

var db_host = Environment.GetEnvironmentVariable("DB_HOST");
var db_name = Environment.GetEnvironmentVariable("DB_NAME");
var db_password = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connString = $"Data Source={db_host};Initial Catalog={db_name};User ID=sa;Password={db_password};TrustServerCertificate=true";

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(connString));
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

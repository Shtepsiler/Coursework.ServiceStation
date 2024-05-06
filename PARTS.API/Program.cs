using PARTS.DAL;
using PARTS.BLL;
using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddPartsDal();
builder.Services.AddPartsBll();
builder.Services.AddDbContext<PartsDBContext>(options =>
{
    string connectionString;

    if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
    {

        var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbname = Environment.GetEnvironmentVariable("DB_NAME");
        var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");


        connectionString = $"Data Source={dbhost};User ID=sa;Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";
    }
    else
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);

});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("DockerRedisConnection");
    options.InstanceName = "ServiceStationParts";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

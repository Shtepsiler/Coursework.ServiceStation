using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Caching.Memory;
using JOBS.DAL.Data;
using JOBS.BLL;
var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBuisnesLogicLayer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*

builder.Services.Configure<MessageBrokerSettings>(
          builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);*/

/*builder.Services.AddTransient<IEventBus, EventBus>();
builder.Services.AddMassTransit(busconf =>
{
    busconf.SetKebabCaseEndpointNameFormatter();
    busconf.AddConsumer<ModelConsumer>();
    busconf.UsingRabbitMq((cont, conf) =>
    {
        MessageBrokerSettings settings = cont.GetRequiredService<MessageBrokerSettings>();

        conf.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);

        });

        conf.ReceiveEndpoint(nameof(GeneralBusMessages.Message.Model), e =>
        {
            e.ConfigureConsumer(cont, typeof(ModelConsumer));
        });
    });


});*/




builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Manager API" });
    /*o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //  o.IncludeXmlComments(xmlPath);
    // Security
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });*/
});

builder.Services.AddDbContext<ServiceStationDContext>(options =>
{
    string connectionString;

    if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
    {

        var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbname = Environment.GetEnvironmentVariable("DB_NAME");
        var dbuser = Environment.GetEnvironmentVariable("DB_USER");
        var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");


        connectionString = $"Data Source={dbhost};User ID={dbuser};Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";
    }
    else
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);

});

builder.Services.AddAuthentication();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Environment.GetEnvironmentVariable("REDIS");
    options.InstanceName = "ServiceStationJobs";
});



/*builder.Services.AddMemoryCache(opt => new MemoryCacheEntryOptions()
{
    AbsoluteExpiration = DateTime.Now.AddSeconds(5),
    Priority = CacheItemPriority.High,
    SlidingExpiration = TimeSpan.FromSeconds(1),
    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1),
    

});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

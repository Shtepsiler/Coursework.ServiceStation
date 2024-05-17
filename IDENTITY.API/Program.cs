using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Google;
using Serilog;
using Serilog.Events;
using IDENTITY.BLL.Factories;
using IDENTITY.BLL.DTO.Requests;
using IDENTITY.BLL.Services.Interfaces;
using IDENTITY.BLL.Services;
using IDENTITY.BLL.Factories.Interfaces;
using IDENTITY.BLL.Configurations;
using IDENTITY.BLL.Mapping;
using IDENTITY.BLL.Validation;
using IDENTITY.DAL.Seeding;
using IDENTITY.DAL.Data;
using IDENTITY.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.File(builder.Configuration["Logging:File:Path"], rollingInterval: RollingInterval.Day)
        .WriteTo.Console()
        ;
});


builder.Services.AddSwaggerGen(o =>
{

    o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Identity Api" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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
    });
});

builder.Services.AddDbContext<AppDBContext>(options =>
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
builder.Services.AddIdentityCore<User>()
                   .AddRoles<Role>()
                   .AddUserManager<UserManager<User>>()
                   .AddSignInManager<SignInManager<User>>()
                   .AddRoleManager<RoleManager<Role>>()
                   .AddDefaultTokenProviders()
                   .AddApiEndpoints()
.AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDBContext>();





builder.Services.AddTransient<JwtTokenConfiguration>();
builder.Services.AddTransient<IJwtSecurityTokenFactory, JwtSecurityTokenFactory>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<ClientAppConfiguration>();

builder.Services.AddTransient<GoogleClientConfiguration>();

builder.Services.AddTransient<EmailSenderConfiguration>();
builder.Services.AddTransient<EmailSender>();

builder.Services.AddScoped<IValidator<UserSignInRequest>, UserSignInRequestValidator>();
builder.Services.AddScoped<IValidator<UserSignUpRequest>, UserSingUpRequestValidator>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddAuthentication(opt =>
{

    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  //  opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  //  opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddCookie(x =>
{
    x.Cookie.Name = "Bearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
        ClockSkew = TimeSpan.FromHours(1),
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Bearer"];
            return Task.CompletedTask;
        }
    };

   


}).AddGoogle(GoogleDefaults.AuthenticationScheme, opt =>
{
    opt.ClientId = builder.Configuration["GoogleClientID"];
    opt.ClientSecret = builder.Configuration["GoogleClientSecret"];
});



builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "Open", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        ;
    });


});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateAsyncScope())
{
    await Seed.Initialize(scope.ServiceProvider);

}

    //app.UseHttpsRedirection();
app.UseCors("Open");
app.UseAuthorization();

app.MapControllers();

app.Run();

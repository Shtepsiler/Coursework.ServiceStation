using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);
var routes = "./Routes";
builder.Configuration.AddOcelotWithSwaggerSupport(p => p.Folder = routes);


//builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
/*builder.Services.AddCors();
*/
builder.Services.AddOcelot();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    /* c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
     c.SwaggerEndpoint("/client/swagger/v1/swagger.json", "Client API");
     c.SwaggerEndpoint("/manager/swagger/v1/swagger.json", "Manager API");*/
});
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseCors();

await app.UseOcelot();
app.UseRouting();
app.UseHttpsRedirection();


app.Run();

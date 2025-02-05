using PassThePigs.Hub.Hubs;
using PassThePigs.Services.Cache;
using PassThePigs.Services.Cache.Interfaces;
using PassThePigs.Services.Interfaces;
using PassThePigs.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PassThePigs.Api", Version = "v1" });
    c.AddSignalRSwaggerGen();
});
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IGameMemoryCache, GameMemoryCache>();
builder.Services.AddTransient<IGameCacheService, GameCacheService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:3000"); // React frontend URL
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline. 
app.UseCors("CorsPolicy");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.MapHub<GameHub>("/GameHub");
Console.WriteLine("SignalR Hub registered at /GameHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
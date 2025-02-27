using PassThePigs.Hub.Hubs;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Services.Interfaces;
using PassThePigs.Services.Services;
using Microsoft.OpenApi.Models;
using PassThePigs.GameLogic.Services;
using PassThePigs.Services.Helpers;
using PassThePigs.Data.Cache;

namespace PassThePigs
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PassThePigs.Api", Version = "v1" });
                c.AddSignalRSwaggerGen();
            });
            services.AddMemoryCache();
            services.AddSingleton<IGameMemoryCache, GameMemoryCache>();
            services.AddTransient<IGameCacheService, GameCacheService>();
            services.AddTransient<IGameLogicService, GameLogicService>();
            services.AddTransient<PlayerLogicHelper, PlayerLogicHelper>();
            services.AddTransient<PigThrowLogicHelper, PigThrowLogicHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "http://localhost:5233");
                    //.SetIsOriginAllowed(origin => true); // React frontend URL, change for variable once deployed
                });
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            }

            app.MapHub<GameHub>("/GameHub");
            app.UseCors("CorsPolicy");

            Console.WriteLine("SignalR Hub registered at /GameHub");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}

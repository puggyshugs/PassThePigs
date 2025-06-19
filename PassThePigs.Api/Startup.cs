using PassThePigs.Hub.Hubs;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Services.Interfaces;
using PassThePigs.Services.Services;
using Microsoft.OpenApi.Models;
using PassThePigs.GameLogic.Services;
using PassThePigs.Services.Helpers;
using PassThePigs.Data.Cache;
using PassThePigs.Services.Helpers.Interfaces;
using PassThePigs.Services;

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
            // services.AddMemoryCache();
            services.AddSingleton<IGameMemoryCache, GameMemoryCache>();
            services.AddScoped<IGameOrchestrationService, GameOrchestrationService>();
            services.AddTransient<IGameCacheService, GameCacheService>();
            services.AddTransient<IGameLogicService, GameLogicService>();
            services.AddTransient<IPlayerLogicHelper, PlayerLogicHelper>();
            services.AddTransient<IPigThrowLogicHelper, PigThrowLogicHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(
                            "http://localhost:5173",        // Vite default
                            "http://localhost:5174",    // Vite alternative
                            "http://localhost:5500",      // listed origin
                            "http://127.0.0.1:5500",      // listed origin
                            "https://localhost:5173",     // HTTPS version
                            "https://localhost:5174",     // HTTPS alternate version
                            "https://localhost:5500",     // HTTPS version
                            "https://127.0.0.1:5500"      // HTTPS version
                        );
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

            app.UseCors("CorsPolicy");
            app.MapHub<GameHub>("/GameHub");

            Console.WriteLine("SignalR Hub registered at /GameHub");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}

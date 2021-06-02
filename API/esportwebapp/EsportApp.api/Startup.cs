using EsportApp.api.Entities;
using EsportApp.api.Helpers;
using EsportApp.api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace EsportApp.api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB configuration
            services.AddDbContext<EsportAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EsportAppContext")));

            // Identity configuration
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<EsportAppContext>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      //builder.WithOrigins("https://localhost:19006", "https://localhost:5001")
                                          .AllowAnyHeader()
                                          .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE");
                                  });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions
                .PropertyNamingPolicy = null;
            });
            services.AddHttpContextAccessor();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "ESportApp API",
                    Description = "REST API for the Esport apps",
                });
            });
            //Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // Configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameTitleRepository, GameTitleRepository>();
            services.AddScoped<IGameTitleTeamRepository, GameTitleTeamRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITeamGameRepository, TeamGameRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            //services.AddScoped<ITornooiGameRepository, TornooiGameRepository>();
            services.AddScoped<ITornooiRepository, TornooiRepository>();
            services.AddScoped<ITornooiTeamRepository, TornooiTeamRepository>();
            services.AddScoped<IUserGameRepository, UserGameRepository>();
            services.AddScoped<IUserGameTitleRepository, UserGameTitleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTeamRepository, UserTeamRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EsportApp API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

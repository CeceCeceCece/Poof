using Application.Exceptions;
using Application.Helpers.Profiles;
using Application.Interfaces;
using Application.Services;
using Application.SignalR;
using Domain;
using Domain.Constants;
using Domain.Entities;
using Hellang.Middleware.ProblemDetails;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PoofDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<Player>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddRoles<IdentityRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<PoofDbContext>();

            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSingleton<PoofTracker>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<ILobbyService, LobbyService>();
            services.AddTransient<IConnectionService, ConnectionService>();

            services.AddAutoMapper(typeof(PoofProfile));

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddAspNetIdentity<Player>()
                .AddProfileService<ProfileService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(opt =>
               {
                   opt.RequireHttpsMetadata = false;
                   opt.Authority = "https://poofapi.azurewebsites.net";
                   opt.Audience = "poof-api";
               });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("AllowedOrigins").Get<string[]>())
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("api-openid", policy => policy.RequireAuthenticatedUser()
                .RequireClaim("scope", "api-openid")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy(PoofRoles.Owner, policy => policy.RequireAuthenticatedUser()
                .RequireClaim(JwtClaimTypes.Role, PoofRoles.Owner)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy(PoofRoles.Admin, policy => policy.RequireAuthenticatedUser()
                .RequireClaim(JwtClaimTypes.Role, PoofRoles.Admin, PoofRoles.Owner)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy(PoofRoles.User, policy => policy.RequireAuthenticatedUser()
                .RequireClaim(JwtClaimTypes.Role, PoofRoles.User, PoofRoles.Admin, PoofRoles.Owner)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.DefaultPolicy = options.GetPolicy(PoofRoles.User);
            });

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false;
                options.Map<PoofException>(
                  (ctx, ex) =>
                  {
                      var pd = StatusCodeProblemDetails.Create(StatusCodes.Status500InternalServerError);
                      pd.Title = ex.Message;
                      return pd;
                  }
               );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PoofHub>("hubs/poof");
                endpoints.MapHub<PoofGameHub>("hubs/poofgame");
            });
        }
    }
}

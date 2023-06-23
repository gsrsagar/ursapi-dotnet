using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using urs_api.DbContexts;
using Newtonsoft.Json;
using urs_api.Models.Implementaions;
using urs_api.Models.Interfaces;
using DataAccessLayer;
using urs_api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using urs_api.Services;
using urs_api.Models;
using Microsoft.OpenApi.Models;

namespace LocalGuideAPI
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
            services.AddDbContext<URSDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("URSDataBase")));
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMvcCore().AddApiExplorer();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });

            services.AddMvc(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();

           //  services.AddScoped<IAuthorizationHandler, RolesAuthorization>();
            services.AddAuthentication("RoleAuthentication")
                .AddCookie("RoleAuthentication", config =>
                 {
                     ///config.AccessDeniedPath = "/Home/Privacy";
                 });
          /*  services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-AU");
            });*/
            // configure strongly typed settings objects
           var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
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
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });
            /*services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Local Guide API's",
                        Description = "",
                        Version = "v1"
                    });

                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                //options.IncludeXmlComments(filePath);
            });*/

            //Configure Swagger

            /*services.AddSwaggerGen();
                   services.ConfigureSwaggerGen(c =>
                   {
                       c.SwaggerDoc("v3", new OpenApiInfo
                       {
                           Title = "GTrackAPI",
                           Version = "v3"
                       });
                   });
*/
            /**/
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            // configure DI for application services
           /**/ services.AddScoped<IAuthenticationService, AuthenticationService>();
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            // configure DI for application services
            services.AddScoped<ILocationService, LocationService>();
            // configure DI for application services
            services.AddScoped<IContactUsService, ContactUsService>();
            // configure DI for application services
            services.AddScoped<IPastProgramService, PastProgramsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, URSDbContext context)
        {

           ////// context.Users.Add(new urs_api.Models.User { FirstName = "Test", LastName = "User", Username = "test", Password = "test" });
          //  context.SaveChanges();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Local Guide API");
                options.RoutePrefix = "";
            });
        }
    }
}

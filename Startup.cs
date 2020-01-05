using System;
using System.Text;
using BecamexIDC.Pattern.EF.Factory;
using BecamexIDC.Pattern.EF.UnitOfWork;
using acb_app.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using acb_app.Models;
using BecamexIDC.Pattern.EF.Repositories;
using BecamexIDC.Pattern.EF.DataContext;
using acb_app.Options;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace acb_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             var identitySettingSection = _configuration.GetSection("AppIdentitySettings");
            var appSettingsSection = _configuration.GetSection("AppSettings");
           services.AddDbContext<ACBSystemContext>(options => options.UseMySql(_configuration.GetConnectionString("MySqlConnection")));
            services.AddDbContext<AuthDbContext>(options => options.UseMySql(_configuration.GetConnectionString("MySqlAuthConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<AuthDbContext>();
            services.AddScoped<IDataContextAsync, ACBSystemContext>();

            // configure strongly typed settings objects      
            services.Configure<AppIdentitySettings>(identitySettingSection);
            services.Configure<AppSettings>(appSettingsSection);
           services.AddScoped<IUnitOfWorkAsync, UnitOfWork>();

             #region  Repository Generic
            
            services.AddScoped<IRepositoryAsync<Product>, Repository<Product>>();    
            services.AddScoped<IRepositoryAsync<Customer>, Repository<Customer>>();  
            services.AddScoped<IRepositoryAsync<Sale>, Repository<Sale>>();  

            services.AddScoped<ICustomerService, CustomerService>();    
            services.AddScoped<IProductService, ProductService>();    
            services.AddScoped<ISaleService, SaleService>();    
            var identitySettings = identitySettingSection.Get<AppIdentitySettings>();
            #endregion 
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var passwordSettings = appSettingsSection.Get<PasswordSettings>();
            var lockoutSettings = appSettingsSection.Get<LockoutSettings>();
            var userSettings = appSettingsSection.Get<UserSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Token);

            services.Configure<IdentityOptions>(options =>
                        {
                            // Password settings
                            options.Password.RequireDigit = passwordSettings.RequireDigit;
                            options.Password.RequiredLength = passwordSettings.RequiredLength;
                            options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                            options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                            options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                            options.Password.RequiredUniqueChars = passwordSettings.RequiredUniqueChars;

                            // Lockout settings
                            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettings.DefaultLockoutTimeSpanInMins);
                            options.Lockout.MaxFailedAccessAttempts = lockoutSettings.MaxFailedAccessAttempts;
                            options.Lockout.AllowedForNewUsers = lockoutSettings.AllowedForNewUsers;

                            // User settings
                            options.User.RequireUniqueEmail = userSettings.RequireUniqueEmail;
                        });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
            services.AddControllers().AddJsonOptions( options =>{
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseCors("CorsPolicy");
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

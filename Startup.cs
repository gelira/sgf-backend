using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SGFBackend.Entities;
using SGFBackend.Helpers;

namespace SGFBackend
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
            var secretKeyConfig = Configuration.Get<SecretKeyConfig>(); 
            var databaseConfig = Configuration.GetSection("DB").Get<DatabaseConfig>(); 
            var key = secretKeyConfig.SecretKeyBytes; 
 
            // Possibilita DI da classe de configuração 
            services.Configure<SecretKeyConfig>(Configuration); 
            // Acesso dos dados no contexto (user_id) 
            services.AddHttpContextAccessor(); 
            services.AddControllers().AddNewtonsoftJson( 
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); 
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
                    ValidateAudience = false 
                }; 
            }); 
            services.AddDbContext<SgfContext>(options =>  
                 options.UseLazyLoadingProxies().UseMySql(databaseConfig.ConnectionString)); 
             // Configuração do Mapper 
            services.AddAutoMapper(typeof(MapperProfile)); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

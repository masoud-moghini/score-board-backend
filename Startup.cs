using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.IdentityModel.Tokens;
using backend.persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace backend
{
    class JwtSecurityKey{
        public static SymmetricSecurityKey Create(string secret) {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var Builder=new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
                .AddEnvironmentVariables();
            Configuration=Builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt=>
                opt.UseSqlServer(Configuration.GetConnectionString("Default"))
            );

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>{
                    cfg.RequireHttpsMetadata=false;
                    cfg.SaveToken = true;
                    
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        
                        // ValidIssuer = "Fiver.Security.Bearer",
                        // ValidAudience = "Fiver.Security.Bearer",
                        IssuerSigningKey = 
                                JwtSecurityKey.Create("fiversecret and more keys ")
                    };
                });
            
            services.AddCors((options)=>options.AddPolicy("Cores",
                (builder)=>{
                builder.
                AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod();
                    }
                    )
                );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("Cores");
            app.UseMvc();
        }

        public void SeedDatabase(ApiContext context){
            context.messages.Add(new Message{
                Owner="masoud",
                text="excellent"
            });

            context.messages.Add(new Message{
                Owner="gholam",
                text="not bad"
            });

            context.SaveChanges();
        }
    }
}

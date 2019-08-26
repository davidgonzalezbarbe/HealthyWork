using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using HealthyWork.API.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HealthyWork
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
            services.AddDbContext<HealthyDbContext>();
            services.AddTransient<IService<Value>, ValueService>();
            services.AddTransient<IService<Room>, RoomService>();
            services.AddTransient<IService<User>, UserService>();
            services.AddTransient<IService<HeadQuarters>, HeadQuartersService>();
            services.AddTransient<IService<TelegramPush>, TelegramPushService>();
            services.AddTransient<IService<Configuration>, ConfigurationService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

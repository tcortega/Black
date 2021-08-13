using Black.Bot;
using Black.Bot.Handler;
using Black.Bot.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TgBotFramework;
using TgBotFramework.Data.EF;

namespace TelegramBot
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.Configure<BotSettings>(Configuration.GetSection(nameof(BlackBot)));
            services.AddScoped<UpdateLogger>();
            services.AddScoped<GlobalExceptionHandler>();
            services.AddScoped<MessageHandler>();

            services.AddDbContext<BotFrameworkContext>(x => x.UseSqlite("Data Source=BlackBot.sqlite"));

            var blackBotAssembly = Assembly.GetAssembly(typeof(BlackBot));
            services.AddBotService<BlackBot, BlackBotContext>(x => x
                .UseLongPolling<PollingManager<BlackBotContext>>(new LongPollingOptions())
                .UseMiddleware<UpdateLogger>()
                .UseMiddleware<GlobalExceptionHandler>()
                .UseStates(blackBotAssembly)
                .UseEF()
                .UseCommands(blackBotAssembly));
                //.SetPipeline(pipeLineBuilder => pipeLineBuilder.MapWhen(On.Message)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

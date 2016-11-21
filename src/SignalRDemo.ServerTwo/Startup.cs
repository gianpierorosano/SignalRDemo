using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SignalRDemo.ServerTwo.Startup))]

namespace SignalRDemo.ServerTwo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterRedis();

            app.UseCors(CorsOptions.AllowAll);

            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJSONP = true
            };

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(hubConfiguration);
            });
        }

        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
        private void RegisterRedis()
        {
            var redisServer = WebConfigurationManager.AppSettings["RedisServer"];
            var redisPort = WebConfigurationManager.AppSettings["RedisPort"] != null
               ? Convert.ToInt32(WebConfigurationManager.AppSettings["RedisPort"])
               : 80;
            var redisKey = WebConfigurationManager.AppSettings["RedisKey"];
            var redisChannel = WebConfigurationManager.AppSettings["RedisChannel"];

            if (string.IsNullOrEmpty(redisServer)
                || string.IsNullOrEmpty(redisChannel))
                return;

            if (string.IsNullOrEmpty(redisKey)) redisKey = null;
            GlobalHost.DependencyResolver.UseRedis(new RedisScaleoutConfiguration(redisServer, redisPort, redisKey, redisChannel));
        }
    }
}
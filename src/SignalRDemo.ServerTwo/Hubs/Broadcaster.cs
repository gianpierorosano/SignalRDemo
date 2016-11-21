using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Web.Configuration;
using Microsoft.AspNet.SignalR;
using SignalRDemo.ServerTwo.Models;

namespace SignalRDemo.ServerTwo.Hubs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Broadcaster
    {
        #region Singleton

        private static readonly Lazy<Broadcaster> _instance =
            new Lazy<Broadcaster>(() => new Broadcaster());

        private static readonly string ServerName = WebConfigurationManager.AppSettings["ServerName"]
            ?? string.Empty;

        public static Broadcaster Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion

        private readonly TimeSpan BroadcastInterval =
           TimeSpan.FromMilliseconds(1000);
        private readonly IHubContext _hubContext;
        private Timer _broadcastLoop;
        private int startXPoint;

        private Broadcaster()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
        }

        public void StartLiveChart()
        {
            _broadcastLoop = new Timer(
                BroadcastDataPoint,
                null,
                BroadcastInterval,
                BroadcastInterval);
        }

        public void StopLiveChart()
        {
            _broadcastLoop.Dispose();
        }

        private void BroadcastDataPoint(object o)
        {
            var rnd = new Random();
            startXPoint++;

            var model = new DataPoint
            {
                X = startXPoint,
                Y = rnd.Next(10, 23),
                ServerName = ServerName
            };
            _hubContext.Clients.All.updateChart(model);
        }
    }
}
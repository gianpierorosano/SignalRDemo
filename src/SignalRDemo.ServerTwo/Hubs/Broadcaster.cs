using System;
using System.Diagnostics.CodeAnalysis;
using System.Timers;
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

        private const int BroadcastInterval = 1000;
        private readonly IHubContext _hubContext;
        private readonly Timer _broadcastLoop;
        private int startXPoint;

        private Broadcaster()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            _broadcastLoop = new Timer { Interval = BroadcastInterval };
            _broadcastLoop.Elapsed += BroadcastDataPoint;
            _broadcastLoop.AutoReset = false;
        }

        public void StartLiveChart()
        {
            _broadcastLoop.Start();
        }

        public void StopLiveChart()
        {
            _broadcastLoop.Stop();
        }

        private void BroadcastDataPoint(object sender, ElapsedEventArgs e)
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
            _broadcastLoop.Start();
        }
    }
}
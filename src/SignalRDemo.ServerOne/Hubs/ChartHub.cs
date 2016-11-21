using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRDemo.ServerOne.Hubs
{
    [HubName("chartHub")]
    public class ChartHub : Hub
    {
        private readonly Broadcaster _broadcaster;

        public ChartHub()
            : this(Broadcaster.Instance)
        {
        }

        private ChartHub(Broadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public void StartLiveChart()
        {
            _broadcaster.StartLiveChart();
        }

        public void StopLiveChart()
        {
            _broadcaster.StopLiveChart();
        }
    }
}
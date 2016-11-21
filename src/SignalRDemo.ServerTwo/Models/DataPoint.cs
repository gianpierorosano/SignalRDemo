using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace SignalRDemo.ServerTwo.Models
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class DataPoint
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("serverName")]
        public string ServerName { get; set; }
    }
}
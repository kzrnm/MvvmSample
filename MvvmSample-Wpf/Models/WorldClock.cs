using System;
using System.Text.Json.Serialization;

namespace Kzrnm.MvvmSample.Wpf.Models
{
    public class WorldClock
    {
        [JsonPropertyName("currentFileTime")]
        public long CurrentFileTime { get; set; }
        [JsonIgnore]
        public DateTime DateTime => DateTime.FromFileTime(CurrentFileTime);
        [JsonPropertyName("timeZoneName")]
        public string? TimeZoneName { get; set; }
    }
}

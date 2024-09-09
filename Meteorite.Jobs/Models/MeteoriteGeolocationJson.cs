using System.Text.Json.Serialization;

namespace Meteorite.Jobs.Models
{
    public class MeteoriteGeolocationJson
    {
        [JsonPropertyName("type")]
        public string GeolocationType { get; set; }
        public float[] Coordinates { get; set; }
    }
}

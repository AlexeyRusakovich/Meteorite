using System.Text.Json.Serialization;

namespace Meteorite.Jobs.Models
{
    public class MeteoriteJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public string RecClass { get; set; }
        public double? Mass { get; set; }
        public string Fall { get; set; }
        public DateTime? Year { get; set; }
        public string RecLat { get; set; }
        public string RecLong { get; set; }
        public MeteoriteGeolocationJson Geolocation { get; set; }
        [JsonPropertyName(":@computed_region_cbhk_fwbd")]
        public string Computed_Region_Cbhk_Fwbd { get; set; }
        [JsonPropertyName(":@computed_region_nnqa_25f4")]
        public string Computed_Region_Nnqa_25f4 { get; set; }
    }
}

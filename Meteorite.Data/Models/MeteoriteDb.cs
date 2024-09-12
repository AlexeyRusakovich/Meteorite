using System.ComponentModel.DataAnnotations;

namespace Meteorite.Data.Models
{
    [Dapper.Extra.Annotations.Table("Meteorites")]
    public class MeteoriteDb
    {
        [Dapper.Extra.Annotations.Key(autoIncrement: false)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(10)]
        public string? NameType { get; set; }

        [MaxLength(30)]
        public string? RecClass { get; set; }

        public double? Mass { get; set; }

        [MaxLength(5)]
        public string? Fall { get; set; }

        public int? Year { get; set; }

        [MaxLength(15)]
        public string? RecLat { get; set; }

        [MaxLength(15)]
        public string? RecLong { get; set; }

        [MaxLength(10)]
        public string? GeolocationType { get; set; }

        [MaxLength(10)]
        public string? Computed_Region_Cbhk_Fwbd { get; set; }

        [MaxLength(10)]
        public string? Computed_Region_Nnqa_25f4 { get; set; }

        [MaxLength(64)]
        public string HashCode { get; set; }
    }
}

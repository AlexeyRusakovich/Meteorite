using Meteorite.Data.Models;
using Meteorite.Jobs.Enums;

namespace Meteorite.Jobs.Models
{
    public class MeteoritesStatuses
    {
        public IEnumerable<(MeteoriteStatus Status, IEnumerable<MeteoriteDb> Items)> MetheoriteStatuses { get; set; } = [];
    }
}

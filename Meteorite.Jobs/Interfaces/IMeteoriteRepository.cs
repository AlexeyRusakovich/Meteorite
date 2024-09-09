using Meteorite.Data.Models;

namespace Meteorite.Jobs.Interfaces
{
    public interface IMeteoriteRepository
    {
        Task CreateMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites);
        Task UpdateMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites);
        Task DeleteMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites);
    }
}

using Meteorite.Api.Models;

namespace Meteorite.Api.Interfaces
{
    public interface IMeteoriteRepository
    {
        Task<IEnumerable<MeteoriteDataGroupedDto>> GetMeteoritesDataGrouped(MeteoriteFilter filter);
        Task<MeteoritesDictionaries> GetMeteoritesDictionaries();
    }
}

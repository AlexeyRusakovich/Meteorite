using Meteorite.Data.Models;

namespace Meteorite.Jobs.Interfaces
{
    public interface IMeteoriteDataLoader
    {
        Task<IEnumerable<MeteoriteDb>> LoadMeteoritesData();
    }
}

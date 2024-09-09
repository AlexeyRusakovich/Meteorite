using Meteorite.Data.Models;
using Meteorite.Jobs.Models;
using System.Data;

namespace Meteorite.Jobs.Interfaces
{
    public interface IMeteoriteCache
    {
        Task<MeteoritesStatuses> GetMeteoritesStatuses(IDbConnection dbConnection, IEnumerable<MeteoriteDb> meteorites);

        void UpdateCache(IEnumerable<MeteoriteDb> meteorites);
    }
}

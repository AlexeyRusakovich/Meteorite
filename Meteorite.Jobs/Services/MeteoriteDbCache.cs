using Dapper;
using Meteorite.Data.Models;
using Meteorite.Jobs.Enums;
using Meteorite.Jobs.Extensions;
using Meteorite.Jobs.Interfaces;
using Meteorite.Jobs.Models;
using Serilog;
using System.Data;

namespace Meteorite.Jobs.Services
{
    public class MeteoriteDbCache : IMeteoriteCache
    {
        private Dictionary<int, string> _meteoritesCache = [];

        private const string _getMeteoritesHashCodes = "SELECT [Id], [HashCode] FROM Meteorites";

        public async Task<MeteoritesStatuses> GetMeteoritesStatuses(IDbConnection dbConnection, IEnumerable<MeteoriteDb> meteorites)
        {
            if (!_meteoritesCache.Any())
            {
                await LoadMeteoritesCacheFromDb(dbConnection);
            }
            
            var meteoritesToAdd = new List<MeteoriteDb>();
            
            var meteoritesToUpdate = new List<MeteoriteDb>();

            foreach (var meteorite in meteorites)
            {
                if (!_meteoritesCache.ContainsKey(meteorite.Id))
                {
                    meteoritesToAdd.Add(meteorite);
                }
                else if (_meteoritesCache.ContainsKey(meteorite.Id) 
                      && _meteoritesCache[meteorite.Id] != meteorite.HashCode)
                {
                    meteoritesToUpdate.Add(meteorite);
                }
            }

            var meteoritesDictionary = meteorites.ToDictionary(x => x.Id, x => x);
            var meteoritesToDelete = _meteoritesCache
                .Where(x => !meteoritesDictionary.ContainsKey(x.Key))
                .Select(x => new MeteoriteDb { Id = x.Key });

            Log.Information($"Meteorites cache");
            Log.Information($"{meteoritesToAdd.Count} meteorites to add," +
                            $"{meteoritesToUpdate.Count} meteorites to update," +
                            $"{meteoritesToDelete.Count()} meteorites to delete.");

            return new MeteoritesStatuses
            {
                MetheoriteStatuses = new List<(MeteoriteStatus, IEnumerable<MeteoriteDb>)>
                {
                    (MeteoriteStatus.Create, meteoritesToAdd),
                    (MeteoriteStatus.Update, meteoritesToUpdate),
                    (MeteoriteStatus.Delete, meteoritesToDelete)
                }
            };
        }

        public void UpdateCache(IEnumerable<MeteoriteDb> meteorites)
        {
            _meteoritesCache = meteorites.ToDictionary(x => x.Id, x => x.GetSha256Hash());
        }

        private async Task LoadMeteoritesCacheFromDb(IDbConnection dbConnection)
        {
            var result = await dbConnection.QueryAsync<(int Id, string HashCode)>(_getMeteoritesHashCodes);
            _meteoritesCache = result.ToDictionary(x => x.Id, x => x.HashCode);
        }
    }
}

using Meteorite.Data.Models;
using Meteorite.Jobs.Extensions;
using Meteorite.Jobs.Interfaces;
using Meteorite.Jobs.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net.Http.Json;

namespace Meteorite.Jobs.Services
{
    public class MeteoriteDataLoader : IMeteoriteDataLoader
    {
        private readonly IConfiguration _configuration;

        public MeteoriteDataLoader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<MeteoriteDb>> LoadMeteoritesData()
        {
            var meteoriteDataUrl = _configuration.GetSection("MeteoritesDataUrl").Value;
            var httpClient = new HttpClient();

            var meteoritesJsonData = await httpClient.GetFromJsonAsync<IEnumerable<MeteoriteJson>>(meteoriteDataUrl);
            var meteoritesDbData = ToMeteoritesDb(meteoritesJsonData).ToList();
            meteoritesDbData.SetHashCodes();
            Log.Information($"Loaded {meteoritesDbData.Count()} meteorites data");

            return meteoritesDbData ?? [];
        }

        private IEnumerable<MeteoriteDb> ToMeteoritesDb(IEnumerable<MeteoriteJson> meteorites)
        {
            return meteorites.Select(m => new MeteoriteDb
            {
                Id = m.Id,
                Name = m.Name,
                NameType = m.NameType,
                Fall = m.Fall,
                Mass = m.Mass,
                RecClass = m.RecClass,
                RecLat = m.RecLat,
                RecLong = m.RecLong,
                Year = m.Year?.Year,
                GeolocationType = m.Geolocation?.GeolocationType,
                Computed_Region_Cbhk_Fwbd = m.Computed_Region_Cbhk_Fwbd,
                Computed_Region_Nnqa_25f4 = m.Computed_Region_Nnqa_25f4
            });
        }
    }
}

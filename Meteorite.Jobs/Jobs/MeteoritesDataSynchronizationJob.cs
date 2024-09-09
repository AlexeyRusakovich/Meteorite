using Coravel.Invocable;
using Meteorite.Data.Models;
using Meteorite.Jobs.Enums;
using Meteorite.Jobs.Interfaces;
using System.Data;

namespace MeteoritDataSynchronizationJob
{
    public class MeteoritesDataSynchronizationJob : IInvocable
    {
        private readonly IMeteoriteDataLoader _meteoriteDataLoader;
        private readonly IMeteoriteCache _meteoriteCache;
        private readonly IMeteoriteRepository _meteoriteRepository;
        private readonly IDbConnection _connection;

        public MeteoritesDataSynchronizationJob(
            IMeteoriteDataLoader meteoriteDataLoader,
            IMeteoriteCache meteoriteCache,
            IMeteoriteRepository meteoriteRepository,
            IDbConnection connection)
        {
            _meteoriteDataLoader = meteoriteDataLoader;
            _meteoriteCache = meteoriteCache;
            _meteoriteRepository = meteoriteRepository;
            _connection = connection;
        }

        public async Task Invoke()
        {
            var meteoritesData = await _meteoriteDataLoader.LoadMeteoritesData();
            if (!meteoritesData.Any())
                return;

            var meteoritesStatuses = await _meteoriteCache.GetMeteoritesStatuses(_connection, meteoritesData);

            foreach (var meteoritesStatus in meteoritesStatuses.MetheoriteStatuses)
            {
                await ProcessMeteoritesData(meteoritesStatus);
            }

            _meteoriteCache.UpdateCache(meteoritesData);
        }

        private async Task ProcessMeteoritesData((MeteoriteStatus Status, IEnumerable<MeteoriteDb> Items) meteoritesStatus)
        {
            if (meteoritesStatus.Status == MeteoriteStatus.Create)
            {
                await _meteoriteRepository.CreateMeteoritesBulk(meteoritesStatus.Items);
            }
            else if (meteoritesStatus.Status == MeteoriteStatus.Delete)
            {
                await _meteoriteRepository.DeleteMeteoritesBulk(meteoritesStatus.Items);
            }
            else if (meteoritesStatus.Status == MeteoriteStatus.Update)
            {
                await _meteoriteRepository.UpdateMeteoritesBulk(meteoritesStatus.Items);
            }
        }
    }
}

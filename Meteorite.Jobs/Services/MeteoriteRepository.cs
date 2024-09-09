using Meteorite.Data.Models;
using Meteorite.Jobs.Interfaces;
using Dapper;
using System.Data;

namespace Meteorite.Jobs.Services
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        private readonly IDbConnection _connection;
        private const string _deleteSQL = @"DELETE FROM Meteorites where Id in (@Ids)";

        public MeteoriteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites)
        {
            await ExecuteByChunks(meteorites, 1000, async m => await _connection.BulkInsertAsync(m));
        }

        public async Task DeleteMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites)
        {
            await ExecuteByChunks(meteorites, 1000,
                async m => await _connection.ExecuteAsync(_deleteSQL, new { Ids = m.Select(x => x.Id) }));
        }

        public async Task UpdateMeteoritesBulk(IEnumerable<MeteoriteDb> meteorites)
        {
            await ExecuteByChunks(meteorites, 1000, async m => await _connection.BulkUpdateAsync(m));
        }

        private async Task ExecuteByChunks(IEnumerable<MeteoriteDb> meteorites, int batchSize, Func<IEnumerable<MeteoriteDb>, Task> processMeteorites)
        {
            foreach (var meteoritesChunk in meteorites.Chunk(batchSize))
            {
                await processMeteorites(meteoritesChunk);
            }
        }
    }
}

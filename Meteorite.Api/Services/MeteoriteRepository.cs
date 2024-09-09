using Meteorite.Api.Interfaces;
using Meteorite.Api.Models;
using Meteorite.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Meteorite.Api.Services
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        private readonly MeteoriteContext _context;

        public MeteoriteRepository(MeteoriteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeteoriteDataGroupedDto>> GetMeteoritesDataGrouped(MeteoriteFilter filter)
        {
            var meteoritesQuery = _context.Meteorites.AsQueryable().Where(x => x.Year != null);

            if (filter.YearFrom.HasValue && filter.YearTo.HasValue)
            {
                meteoritesQuery = meteoritesQuery.Where(x => x.Year.Value.Year >= filter.YearFrom && x.Year.Value.Year <= filter.YearTo);
            }

            if (filter.RecClass != null)
            {
                meteoritesQuery = meteoritesQuery.Where(x => x.RecClass != null && x.RecClass == filter.RecClass);
            }

            if (filter.Name != null)
            {
                meteoritesQuery = meteoritesQuery.Where(x => x.Name != null && x.Name.Contains(filter.Name));
            }

            var groupedQuery = meteoritesQuery
                .GroupBy(x => x.Year)
                .Select(x => new MeteoriteDataGroupedDto
                {
                    Year = x.Key.Value.Year,
                    Count = x.Count(),
                    WeightTotal = x.Sum(x => x.Mass)
                })
                .OrderBy(x => x.Year);

            return await groupedQuery.ToListAsync();
        }

        public async Task<MeteoritesDictionaries> GetMeteoritesDictionaries()
        {
            var distinctYears = await _context.Meteorites
                .Where(x => x.Year != null)
                .Select(x => x.Year.Value.Year)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            var distinctRecClasses = await _context.Meteorites
                .Where(x => x.Year != null)
                .Select(x => x.RecClass)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            return new MeteoritesDictionaries
            {
                MeteoritesYears = distinctYears,
                MeteoritesClasses = distinctRecClasses  
            };
        }
    }
}

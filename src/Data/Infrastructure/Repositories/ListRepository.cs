using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using siwar.Data.Items;

namespace siwar.Data.Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly MonitoringContext _context;

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

        public ListRepository(MonitoringContext context) =>
            _context = context;

        public DbSet<List> GetDbSet() =>
             _context.Lists;

        public async Task<IList<Item>> GetItemsByListAsync(string listName) =>
            await _context.Items
                .Where(x => x.List.Name == listName)
                .ToListAsync();

        public async Task AddAsync(Item item) =>
            await _context.Items.AddAsync(item);

        public async Task AddRangeAsync(IList<Item> items) =>
            await _context.Items.AddRangeAsync(items);

        public void Update(Item item) =>
            _context.Items.Update(item);
    }
}

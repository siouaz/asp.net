using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OeuilDeSauron.Data.Items;

namespace OeuilDeSauron.Data.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly MonitoringContext _context;

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

        public ItemRepository(MonitoringContext context) =>
            _context = context;

        public DbSet<Item> GetDbSet() =>
            _context.Items;

        public async Task<IList<Item>> GetByListAsync(string listName) =>
            await _context.Items
                .Where(x => x.List.Name == listName)
                .ToListAsync();

        public async Task<IList<Item>> GetByListIdAsync(int listId) =>
            await _context.Items
                .Where(x => x.ListId == listId)
                .ToListAsync();

        public async Task AddItemAsync(Item item) =>
            await _context.Items.AddAsync(item);

        public void RemoveItems(Item item) =>
            _context.Items.Remove(item);

        public void UpdateItem(Item item) =>
            _context.Items.Update(item);

        public async Task AddItemRelationsAsync(IList<ItemRelation> items) =>
            await _context.ItemRelations.AddRangeAsync(items);

        public void RemoveItemRelations(IList<ItemRelation> items) =>
            _context.ItemRelations.RemoveRange(items);
    }
}

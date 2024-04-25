using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using siwar.Data.Items;
using siwar.Data.Pagination;
using siwar.Domain.Models;

namespace siwar.Domain.Queries
{
    public class ItemQueries : IItemQueries
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemQueries(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<IList<ItemModel>> GetItemsByListAsync(int listId, SortOptions options)
        {
            var query = _itemRepository.GetDbSet()
                .Include(i => i.List)
                .Include(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.List)
                .Include(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.List)
                .Include(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Child)
                .ThenInclude(i => i.List)
                .AsQueryable();
            query = query
                .Where(i => i.ListId == listId);

            query = !string.IsNullOrWhiteSpace(options.Sort)
                ? options.SortDirection == SortDirection.Asc
                    ? query.OrderBy(x => $"x.{options.Sort}")
                    : query.OrderByDescending(x => $"x.{options.Sort}")
                : query.OrderBy(u => u.Id);

            var items = await query
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IList<Item>, IList<ItemModel>>(items);
        }
        public async Task<ItemModel> GetItemByIdAsync(int id)
        {
            var item = await _itemRepository.GetDbSet()
                .Include(i => i.Parents)
                .ThenInclude(i => i.Parent)
                .Include(i => i.List)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
            return _mapper.Map<ItemModel>(item);
        }
        public async Task<IList<ItemModel>> GetAllAsync()
        {
            var items = await _itemRepository.GetDbSet()
                .Include(i => i.List)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IList<Item>, IList<ItemModel>>(items);
        }

        public async Task<IList<ItemModel>> GetItemsByParentIdAsync(int parentId)
        {
            var items = await _itemRepository.GetDbSet()
                .Where(i => i.Parents.Any(p => p.ParentId == parentId))
                .ToListAsync();
            return _mapper.Map<IList<Item>, IList<ItemModel>>(items);
        }
    }
}

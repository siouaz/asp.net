using System.Collections.Generic;
using System.Threading.Tasks;

using siwar.Data.Pagination;
using siwar.Domain.Models;

namespace siwar.Domain.Queries
{
    public interface IItemQueries
    {
        Task<IList<ItemModel>> GetItemsByListAsync(int listId, SortOptions options);

        Task<ItemModel> GetItemByIdAsync(int id);

        Task<IList<ItemModel>> GetAllAsync();

        Task<IList<ItemModel>> GetItemsByParentIdAsync(int parentId);
    }
}

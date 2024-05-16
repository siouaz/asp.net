using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OeuilDeSauron.Data.Items
{
    /// <summary>
    /// <see cref="List"/> repository.
    /// </summary>
    public interface IListRepository : IRepository<List>
    {
        Task<IList<Item>> GetItemsByListAsync(string listName);

        Task AddAsync(Item item);

        Task AddRangeAsync(IList<Item> items);

        void Update(Item item);
    }
}

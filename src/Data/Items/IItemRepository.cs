using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OeuilDeSauron.Data.Items
{
    /// <summary>
    /// <see cref="Item"/> repository.
    /// </summary>
    public interface IItemRepository : IRepository<Item>
    {
        Task<IList<Item>> GetByListAsync(string listName);

        Task<IList<Item>> GetByListIdAsync(int listId);

        Task AddItemAsync(Item item);

        void RemoveItems(Item item);

        void UpdateItem(Item item);

        Task AddItemRelationsAsync(IList<ItemRelation> items);

        void RemoveItemRelations(IList<ItemRelation> items);

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using OeuilDeSauron.Domain.Models;

namespace OeuilDeSauron.Domain.Queries
{
    public interface IListQueries
    {
        Task<IList<ListModel>> GetAllAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using siwar.Domain.Models;

namespace siwar.Domain.Queries
{
    public interface IListQueries
    {
        Task<IList<ListModel>> GetAllAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using siwar.Data.Items;
using siwar.Domain.Models;

namespace siwar.Domain.Queries
{
    public class ListQueries : IListQueries
    {
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;

        public ListQueries(IListRepository listRepository, IMapper mapper)
        {
            _listRepository = listRepository;
            _mapper = mapper;
        }

        public async Task<IList<ListModel>> GetAllAsync()
        {
            var lists = await _listRepository.GetDbSet()
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IList<List>, IList<ListModel>>(lists);
        }
    }
}

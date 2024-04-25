using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using siwar.Data.Exceptions;
using siwar.Data.Items;
using siwar.Domain.Models;

namespace siwar.Domain.Commands;

public class CreateOrUpdateItem : IRequest<ItemModel>
{
    public ItemModel Item { get; private set; }

    public CreateOrUpdateItem(ItemModel item) =>
        Item = item ?? throw new ArgumentNullException(nameof(item));
}

public class CreateItemHandler : IRequestHandler<CreateOrUpdateItem, ItemModel>
{
    private readonly IItemRepository _itemRepository;
    private readonly IListRepository _listRepository;
    private readonly IMapper _mapper;
    private readonly IResources _resources;

    public CreateItemHandler(
        IItemRepository itemRepository,
        IMapper mapper,
        IResources resources,
        IListRepository listRepository)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _resources = resources;
        _listRepository = listRepository;
    }

    public async Task<ItemModel> Handle(CreateOrUpdateItem request, CancellationToken cancellationToken)
    {
        var list = await _listRepository.GetDbSet().FirstOrDefaultAsync(l => l.Id == request.Item.ListId);
        if (!list.Editable)
        {
            throw new BusinessException(_resources.DeniedListEdit);
        }
        var item = _mapper.Map<ItemModel, Item>(request.Item);
        var updated = await _itemRepository.GetDbSet()
            .Include(l => l.Parents)
            .FirstOrDefaultAsync(x => x.Id == item.Id, cancellationToken);
        if (updated is null)
        {
            if (await _itemRepository.GetDbSet().AnyAsync(x => x.Code == item.Code, cancellationToken))
            {
                throw new BusinessException(_resources.UniqueKeyException);
            }
            await _itemRepository.AddItemAsync(item);
        }
        else
        {
            if (await _itemRepository.GetDbSet().AnyAsync(x => x.Code == item.Code && x.Id != item.Id, cancellationToken))
            {
                throw new BusinessException(_resources.UniqueKeyException);
            }
            updated.Update(item);
            // update parents
            var removedParents = updated.Parents.Except(item.Parents);
            if (removedParents.Any())
            {
                _itemRepository.RemoveItemRelations(removedParents.ToList());
            }
            var newParents = item.Parents.Except(updated.Parents);
            if (newParents.Any())
            {
                await _itemRepository.AddItemRelationsAsync(newParents.ToList());
            }
            _itemRepository.UpdateItem(updated);
        }
        await _itemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<Item, ItemModel>(await _itemRepository.GetDbSet().FirstOrDefaultAsync(i => i.Code == request.Item.Code));
    }
}

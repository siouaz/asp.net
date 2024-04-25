using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using siwar.Data.Items;

namespace siwar.Domain.Commands;

public class RemoveItem : IRequest<bool>
{
    public int ItemId { get; private set; }

    public RemoveItem(int itemId) =>
        ItemId = itemId;
}

public class RemoveItemHandler : IRequestHandler<RemoveItem, bool>
{
    private readonly IItemRepository _itemRepository;

    public RemoveItemHandler(IItemRepository itemRepository) =>
        _itemRepository = itemRepository;

    public async Task<bool> Handle(RemoveItem request, CancellationToken cancellationToken)
    {
        var deletedItem = await _itemRepository.GetDbSet()
            .Include(c => c.Parents)
            .Include(c => c.Children)
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);
        deletedItem.Parents.Clear();
        deletedItem.Children.Clear();
        _itemRepository.RemoveItems(deletedItem);
        await _itemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MediatR;

using siwar.Data.Infrastructure;
using siwar.Domain.Models.Identity;
using siwar.Domain.Services;

namespace siwar.Domain.Commands.Identity
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// User id from route.
        /// </summary>
        public string UserId { get; }
        /// <summary>
        /// User request.
        /// </summary>
        public UserModel User { get; }
        public UpdateUserCommand(string userId, UserModel user)
        {
            UserId = userId;
            User = user;
        }
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly siwarContext _siwarContext;
        private readonly IUserService _userService;

        public UpdateUserHandler(
            siwarContext siwarContext,
            IUserService userService)
        {
            _siwarContext = siwarContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Transaction
            // Ref : https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/work-with-data-in-asp-net-core-apps#execution-strategies-and-explicit-transactions-using-begintransaction-and-multiple-dbcontexts
            var strategy = _siwarContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _siwarContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await _userService.UpdateUserAsync(request.User.Id, request.User, cancellationToken);
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });

            // MediatR void
            return Unit.Value;
        }
    }
}

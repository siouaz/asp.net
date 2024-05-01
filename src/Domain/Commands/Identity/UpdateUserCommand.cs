using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MediatR;

using OeuilDeSauron.Data.Infrastructure;
using OeuilDeSauron.Domain.Models.Identity;
using OeuilDeSauron.Domain.Services;
using OeuilDeSauron.Data;

namespace OeuilDeSauron.Domain.Commands.Identity
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
        private readonly MonitoringContext _MonitoringContext;
        private readonly IUserService _userService;

        public UpdateUserHandler(
            MonitoringContext MonitoringContext,
            IUserService userService)
        {
            _MonitoringContext = MonitoringContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Transaction
            // Ref : https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/work-with-data-in-asp-net-core-apps#execution-strategies-and-explicit-transactions-using-begintransaction-and-multiple-dbcontexts
            var strategy = _MonitoringContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _MonitoringContext.Database.BeginTransactionAsync(cancellationToken);
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

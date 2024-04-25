using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MediatR;

using siwar.Data.Infrastructure;
using siwar.Domain.Interfaces;
using siwar.Domain.Models.Identity;
using siwar.Domain.Services;
using siwar.Data;

namespace siwar.Domain.Commands.Identity
{
    public class UpdateProfileCommand : IRequest<Unit>
    {
        public ProfileModel Profile { get; set; }
        public UpdateProfileCommand(ProfileModel profile)
        {
            Profile = profile;
        }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly MonitoringContext _MonitoringContext;
        private readonly IUserService _userService;
        public UpdateProfileCommandHandler(ICurrentUserService currentUserService, MonitoringContext MonitoringContext, IUserService userService)
        {
            _currentUserService = currentUserService;
            _MonitoringContext = MonitoringContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken = default)
        {
            // Transaction
            // Ref : https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/work-with-data-in-asp-net-core-apps#execution-strategies-and-explicit-transactions-using-begintransaction-and-multiple-dbcontexts
            var strategy = _MonitoringContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _MonitoringContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await _userService.UpdateProfileAsync(_currentUserService.UserId, request.Profile, cancellationToken);
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });

            return Unit.Value;
        }
    }
}

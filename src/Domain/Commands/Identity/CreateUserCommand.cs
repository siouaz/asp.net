using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using siwar.Data.Infrastructure;
using siwar.Domain.Models.Identity;
using siwar.Domain.Services;
using System;
using siwar.Data;

namespace siwar.Domain.Commands.Identity
{
    public class CreateUserCommand : IRequest<string>
    {
        /// <summary>
        /// User request.
        /// </summary>
        public UserModel User { get; }
        public CreateUserCommand(UserModel user)
        {
            User = user;
        }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly MonitoringContext _MonitoringContext;
        private readonly IUserService _userService;

        public CreateUserHandler(
            MonitoringContext MonitoringContext,
            IUserService userService)
        {
            _MonitoringContext = MonitoringContext;
            _userService = userService;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Transaction
            //var strategy = _MonitoringContext.Database.CreateExecutionStrategy();
            //var result = await strategy.ExecuteAsync(async () =>
            //{
            //    using var transaction = await _MonitoringContext.Database.BeginTransactionAsync(cancellationToken);
            //    try
            //    {
            //        var userId = await _userService.CreateUserAsync(request.User, cancellationToken);
            //        await transaction.CommitAsync(cancellationToken);
            //        return userId;
            //    }
            //    catch
            //    {
            //        await transaction.RollbackAsync(cancellationToken);
            //        throw;
            //    }
            //});

            throw new NotImplementedException();
        }
    }
}

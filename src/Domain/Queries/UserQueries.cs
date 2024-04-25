using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using siwar.Data.Identity;
using siwar.Data.Infrastructure;
using siwar.Domain.Models.Identity;
using siwar.Data.Pagination;
using siwar.Domain.Interfaces;
using siwar.Domain.Extensions;
using siwar.Data;

namespace siwar.Domain.Queries;

public class UserQueries : IUserQueries
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly MonitoringContext _MonitoringContext;
    private readonly UserManager<User> _userManager;

    public UserQueries(ICurrentUserService currentUserService, IMapper mapper, MonitoringContext MonitoringContext,
        UserManager<User> userManager)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _MonitoringContext = MonitoringContext;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<PagedList<UserModel>> GetAllAsync(FilterPaginationOptions options,
        CancellationToken cancellationToken = default)
    {
        var query = await GetAllEligibleQueryableAsync(cancellationToken);

        // Retrieve authenticated user information
        var authenticatedUser = await GetByIdEntityAsync(_currentUserService.UserId, cancellationToken);

        // Search
        // First name + Last name
        // Email
        if (!string.IsNullOrEmpty(options.Search))
        {
            var upperCasedSearch = options.Search.Trim().ToUpper();
            query = query.Where(x =>
                x.Email.ToUpper().Contains(upperCasedSearch) || x.FirstName.ToUpper().Contains(upperCasedSearch) ||
                x.LastName.ToUpper().Contains(upperCasedSearch));
        }

        // Filters
        if (options.Filters is not null)
        {
            foreach (var filter in options.Filters)
            {
                // TO DO
            }
        }

        // Sorting
        query = !string.IsNullOrWhiteSpace(options.Sort)
            ? options.SortDirection == SortDirection.Asc
                ? query.OrderByDynamic(x => $"x.{options.Sort}")
                : query.OrderByDescendingDynamic(x => $"x.{options.Sort}")
            : query.OrderBy(x => x.Id);

        var users = await query
            .Skip(options.Skip)
            .Take(options.Take)
            .Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Telephone = x.Telephone,
                BirthDate = x.BirthDate,
                LockoutEnd = x.LockoutEnd
            })
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Count
        var count = query.Count();

        // Map and client evaluation
        var userModels = _mapper.Map<IList<User>, IList<UserModel>>(users);

        var queriedUserIds = userModels.Select(x => x.Id).ToList();

        return new PagedList<UserModel>(options.Take, options.Skip, count, userModels);
    }

    /// <inheritdoc />
    public async Task<User> GetEligibleByIdEntityAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return user;
    }

    public async Task<UserModel> GetByIdForAdminInterfaceAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var user = await GetByIdForAdminInterfaceEntityAsync(id, cancellationToken);

        return _mapper.Map<User, UserModel>(user);
    }

    /// <inheritdoc />
    public async Task<User> GetByIdForAdminInterfaceEntityAsync(string id,
        CancellationToken cancellationToken = default)
    {
        // Check if user can query the user.
        var authenticatedUser = await GetEligibleByIdEntityAsync(_currentUserService.UserId, cancellationToken);
        // Restrict to managers for now
        var ignoredRoles = new List<string>
        {
            Roles.InspectionOrganismValidInspector, Roles.ValidInspectionOrganismValidInspector
        };

        var query = _userManager.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .AsQueryable();

        var nowPlusOneHour = DateTimeOffset.UtcNow.AddHours(1);

        var user = await query.AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return user;
    }

    /// <inheritdoc />
    public async Task<User> GetByIdEntityAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        return user;
    }

    public async Task<IQueryable<User>> GetAllEligibleQueryableAsync(CancellationToken cancellationToken = default)
    {
        var authenticatedUser = await GetEligibleByIdEntityAsync(_currentUserService.UserId, cancellationToken);

        // If admin or draaf, no filter
        if (authenticatedUser.Roles.Any(x =>
                x.RoleId.Equals(Roles.GlobalAdministratorRole) || x.RoleId.Equals(Roles.DraafManager)))
        {
            return _userManager.Users
                .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                .AsSplitQuery()
                .AsNoTracking();
        }

        // Restrict to managers for now
        var allowedRoles = new List<string>
        {
            Roles.ValidInspectionOrganismManager, Roles.ValidTrainingCenterManager
        };

        // user is not locked out and
        // is attached to the same structure as authenticated user or
        // has a certification delivered by the authenticated structure
        return _userManager.Users
            .Include(x => x.Roles)
            .ThenInclude(x => x.Role)
            .Where(x => !x.LockoutEnd.HasValue || x.LockoutEnd.Value < DateTimeOffset.UtcNow.AddHours(1))
            .AsSplitQuery()
            .AsNoTracking();
    }

    /// <inheritdoc />
    public async Task<ProfileModel> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException();
        }

        var user = await GetEligibleByIdEntityAsync(_currentUserService.UserId, cancellationToken);

        if (user.IsLockedOut)
        {
            throw new UnauthorizedAccessException();
        }

        var profile = _mapper.Map<User, ProfileModel>(user);
        return profile;
    }
}

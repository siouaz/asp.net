using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using siwar.Data.Identity;
using siwar.Domain.Models.Identity;
using System.Threading;
using siwar.Domain.Interfaces;
using System.Security.Claims;

namespace siwar.Domain.Queries
{
    public class RoleQueries : IRoleQueries
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        public RoleQueries(ICurrentUserService currentUserService, IMapper mapper, RoleManager<Role> roleManager)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        /// <inheritdoc />
        public async Task<IList<RoleModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            return _mapper.Map<IList<Role>, IList<RoleModel>>(roles);
        }

        /// <inheritdoc />
        public async Task<IList<Role>> GetAllEligibleRolesAsync(CancellationToken cancellationToken = default)
        {
            var userRoles = _currentUserService.User.Claims.Where(x => x.Type.Equals(ClaimTypes.Role))
                .Select(x => x.Value);
            var eligibleRoles = userRoles.Aggregate(new List<string>(), (accumulator, current) =>
            {
                if (!Eligibility.RolesByRole.TryGetValue(current, out var currentEligibleRoles))
                {
                    return accumulator;
                }
                var newValue = currentEligibleRoles.Except(accumulator);
                accumulator.AddRange(newValue);
                return accumulator;
            });

            var roles = await _roleManager.Roles.Where(x => eligibleRoles.Contains(x.Id)).AsNoTracking().ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<IList<RoleModel>> GetAllEligibleRoleModelsAsync(CancellationToken cancellationToken = default)
        {
            var userRoles = _currentUserService.User.Claims.Where(x => x.Type.Equals(ClaimTypes.Role)).Select(x => x.Value).ToList();

            var eligibleRoles = await GetAllEligibleRolesAsync(cancellationToken);

            return _mapper.Map<IList<Role>, IList<RoleModel>>(eligibleRoles);
        }
    }
}

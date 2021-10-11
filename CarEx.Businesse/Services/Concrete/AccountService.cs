using CarEx.Business.Repository;
using CarEx.Business.Services.Abstract;
using CarEx.Core.Model;
using CarEx.Data.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarEx.Business.Services.Concrete
{
    public class AccountService : Repository<Account>, IAccountService
    {
        private readonly CarExDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountService(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,CarExDbContext context) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(Account account, string password)
        {
            var result = await _userManager.CreateAsync(account, password);
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(Account account, string role)
        {
           return await _userManager.AddToRoleAsync(account, role);
        }

        public List<IdentityRole> GetRoles() {
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }
    }
}

using CarEx.Business.Repository;
using CarEx.Core.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarEx.Business.Services.Abstract
{
    public interface IAccountService : IRepository<Account>
    {
        public  Task<IdentityResult> CreateAsync(Account account, string password);
        public Task<IdentityResult> AddToRoleAsync(Account account, string role);
        public List<IdentityRole> GetRoles();
        public Task<IdentityResult> CreateRoleAsync(IdentityRole role);
    }
}

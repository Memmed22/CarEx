using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Log.Model;
using CarEx.Core.Model;
using CarEx.Core.ViewModel;
using CarEx.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarEx.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogEngine _logger;
       
        public AccountController(IUnitOfWork unitOfWork, ILogEngine logger, UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] AccountViewModel accountViewModel) {
            try
            {
                if (!_roleManager.Roles.Any(u => u.Name == EnumRole.Client.ToString()))
                    _roleManager.CreateAsync(new IdentityRole { Name = EnumRole.Client.ToString() }).GetAwaiter().GetResult();

                Account account = new Account()
                {
                    UserName = accountViewModel.UserName,
                    Password = accountViewModel.Password,
                    CreatedOn = DateTime.Now,
                    AccountType = EnumAccountType.USER.ToString()
                };

                var result = await _userManager.CreateAsync(account, accountViewModel.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(account, EnumRole.Client.ToString()).GetAwaiter().GetResult();

                    _unitOfWork.User.Add(new User
                    {
                        AccountId = account.Id,
                        Adress = accountViewModel.Adress,
                        Email = accountViewModel.UserName,
                        Name = accountViewModel.Name,
                        Photo = accountViewModel.Photo,
                        Surname = accountViewModel.Surname,
                        TelNo = accountViewModel.TelNo,
                        ClientCode = accountViewModel.ClientCode,
                        PersonId = accountViewModel.PersonId,
                    });
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _logger.SendError(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountViewModel accountViewModel)
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                foreach (var item in Enum.GetValues(typeof(EnumRole)).Cast<EnumRole>().ToList())
                {
                    if (!roles.Any(u => u.Name == item.ToString()))
                    {
                        _roleManager.CreateAsync(new IdentityRole(item.ToString())).GetAwaiter().GetResult();
                    }
                }
               
                Account account = new Account()
                {
                    UserName = accountViewModel.UserName,
                    Password = accountViewModel.Password,
                    CreatedOn = DateTime.Now,
                    AccountType = accountViewModel.AccountType
                };

                var result = await _userManager.CreateAsync(account,accountViewModel.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(account, accountViewModel.Role.ToString()).GetAwaiter();
                }

                if (accountViewModel.AccountType == EnumAccountType.EMPLOYEE.ToString())
                {
                    Employee employee = new Employee()
                    {
                        AccountId = account.Id,
                        Adress = accountViewModel.Adress,
                        Email = accountViewModel.UserName,
                        Name = accountViewModel.Name,
                        Photo = accountViewModel.Photo,
                        Surname = accountViewModel.Surname,
                        TelNo = accountViewModel.TelNo,

                    };
                    _unitOfWork.Employee.Add(employee);
                    _unitOfWork.Save();
                }
                
            }
            catch (Exception ex)
            {
                _logger.SendError(ex.Message);
            }
            return Ok(accountViewModel.UserName);
        }
    }
}
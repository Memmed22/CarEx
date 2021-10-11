using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Log.Model;
using CarEx.Core.Model;
using CarEx.Core.Dto;
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
       
        public AccountController(IUnitOfWork unitOfWork, ILogEngine logger)
        {
           
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost("RegisterClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RegisterClient([FromBody] AccountDto accountDto) {
            try
            {
                if (accountDto == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);

                if (!_unitOfWork.Account.GetRoles().Any(u => u.Name == EnumRole.Client.ToString()))
                    _unitOfWork.Account.CreateRoleAsync(new IdentityRole { Name = EnumRole.Client.ToString() }).GetAwaiter().GetResult();

                Account account = new Account()
                {
                    UserName = accountDto.UserName,
                    Password = accountDto.Password,
                    CreatedOn = DateTime.Now,
                    AccountType = EnumAccountType.USER.ToString()
                };

                //  var result = await _userManager.CreateAsync(account, accountDto.Password);
                var result = await _unitOfWork.Account.CreateAsync(account, accountDto.Password);
                if (result.Succeeded)
                {
                    //_userManager.AddToRoleAsync(account, EnumRole.Client.ToString()).GetAwaiter().GetResult();
                    _unitOfWork.Account.CreateAsync(account, EnumRole.Client.ToString()).GetAwaiter().GetResult();

                    _unitOfWork.User.Add(new Client
                    {
                        AccountId = account.Id,
                        Adress = accountDto.Adress,
                        Email = accountDto.UserName,
                        Name = accountDto.Name,
                        Photo = accountDto.Photo,
                        Surname = accountDto.Surname,
                        TelNo = accountDto.TelNo,
                        ClientCode = accountDto.ClientCode,
                        PersonalId = accountDto.PersonId,
                    });
                    _unitOfWork.Save();
                }
                else
                    return UnprocessableEntity(result.Errors);
            }
            catch (Exception ex)
            {
               return Ok(_logger.SendError(ex.Message));
            }
            return Ok(accountDto.UserName);

        }

        [HttpPost("RegisterEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RegisterEmployee([FromBody] AccountDto accountDto)
        {
            try
            {
                if (accountDto == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);

                var roles = _unitOfWork.Account.GetRoles();
                foreach (var item in Enum.GetValues(typeof(EnumRole)).Cast<EnumRole>().ToList())
                {
                    if (!roles.Any(u => u.Name == item.ToString()))
                    {
                        _unitOfWork.Account.CreateRoleAsync(new IdentityRole(item.ToString())).GetAwaiter().GetResult();
                    }
                }
               
                Account account = new Account()
                {
                    UserName = accountDto.UserName,
                    Password = accountDto.Password,
                    CreatedOn = DateTime.Now,
                    AccountType = accountDto.AccountType
                };

                var result = await _unitOfWork.Account.CreateAsync(account, accountDto.Password);
                if (result.Succeeded)
                {
                 _unitOfWork.Account.AddToRoleAsync(account, accountDto.Role.ToString()).GetAwaiter().GetResult();

                    Employee employee = new Employee()
                    {
                        AccountId = account.Id,
                        Adress = accountDto.Adress,
                        Email = accountDto.UserName,
                        Name = accountDto.Name,
                        Photo = accountDto.Photo,
                        Surname = accountDto.Surname,
                        TelNo = accountDto.TelNo,
                        
                    };
                    _unitOfWork.Employee.Add(employee);
                    _unitOfWork.Save();
                }
                else
                    return UnprocessableEntity(result.Errors);
                
            }
            catch (Exception ex)
            {
               return Ok(_logger.SendError(ex.Message));
            }
            return Ok(accountDto.UserName);
        }
    }
}
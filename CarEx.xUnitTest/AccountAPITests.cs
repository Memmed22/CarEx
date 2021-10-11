using CarEx.Business.UnitOfWork;
using CarEx.Core.Dto;
using CarEx.Core.Log.Business;
using CarEx.Core.Model;
using CarEx.Utility;
using CarEx.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarEx.xUnitTest
{

    public class AccountAPITests
    {
        private readonly AccountController _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        // private readonly Mock<UserManager<Account>> _userManagerMock = new Mock<UserManager<Account>>();
        // private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock = new Mock<RoleManager<IdentityRole>>();
        private readonly Mock<ILogEngine> _loggerMock = new Mock<ILogEngine>();

        public AccountAPITests()
        {
            _sut = new AccountController(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task RegisterEmployee_AnAccountWillBeRegistred_AnEmployeeWillBeRegistered_ReturnAddedUserName_ReturnStatusCode200()
        {
            //Arrange
            AccountDto model = new AccountDto()
            {
                AccountType = EnumAccountType.EMPLOYEE.ToString(),
                Adress = "Test - Adress",
                ClientCode = "Test - Client Code",
                Name = "Test",
                Password = "Test12345",
                PersonId = "3456787654",
                Photo = "Test - Photo",
                Role = EnumRole.Editor.ToString(),
                Surname = "Test - Surname",
                TelNo = "+99999999",
                UserName = "test@test.com"
            };

            List<IdentityRole> roles = new List<IdentityRole>() { new IdentityRole("Admin"), new IdentityRole("Editor") };


            string accountId = "34567898765";
            Account account = new Account
            {
                Id = accountId,
                UserName = "test@test.com",
                Password = "Test12345",
                CreatedOn = DateTime.Now.AddDays(2),
            };

            var us = new Account();

            _unitOfWorkMock.Setup(x => x.Account.GetRoles()).Returns(roles);
            _unitOfWorkMock.Setup(x => x.Account.CreateAsync(It.IsAny<Account>(), model.Password)).ReturnsAsync(IdentityResult.Success).Callback<Account, string>((x, y) => us.UserName = x.UserName);
            _unitOfWorkMock.Setup(x => x.Employee.Add(It.IsAny<Employee>())).Verifiable();
            //Act

            var result = await _sut.RegisterEmployee(model);
            //Assert

            var actionValue = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(model.UserName, (actionValue.Value as string));
            Assert.Equal(200, (actionValue.StatusCode));
        }

        [Fact]
        public async Task RegisterEmployee_PassNullValue_ReturnBadRequest_ReturnStatusCode400() {

            //Arrange
            _unitOfWorkMock.Setup(x => x.Account.GetRoles()).Returns(It.IsAny<List<IdentityRole>>());
            _unitOfWorkMock.Setup(x => x.Account.CreateAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _unitOfWorkMock.Setup(x => x.Employee.Add(It.IsAny<Employee>())).Verifiable();

            //Act
            var result = await _sut.RegisterEmployee(null);

            //Assert

            var resultBadRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, resultBadRequest.StatusCode);
        }

        [Fact]
        public async Task RegisterClient_AccountWillBeRegistered_UserWillBeRegistered_ReturnUserName_ReturnStatusCode200()
        {
            //Arrange
            AccountDto model = new AccountDto()
            {
                AccountType = EnumAccountType.EMPLOYEE.ToString(),
                Adress = "Test - Adress",
                ClientCode = "Test - Client Code",
                Name = "Test",
                Password = "Test12345",
                PersonId = "3456787654",
                Photo = "Test - Photo",
                Role = EnumRole.Editor.ToString(),
                Surname = "Test - Surname",
                TelNo = "+99999999",
                UserName = "test@test.com"
            };

            _unitOfWorkMock.Setup(x => x.Account.GetRoles()).Returns(It.IsAny<List<IdentityRole>>());
            _unitOfWorkMock.Setup(x => x.Account.CreateAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _unitOfWorkMock.Setup(x => x.Employee.Add(It.IsAny<Employee>())).Verifiable();

            //Act

            var result = await _sut.RegisterClient(model);


            //Assert

            var resultValue = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(model.UserName, (resultValue.Value as string));
            Assert.Equal(200, resultValue.StatusCode);
        }

        [Fact]
        public async Task RegisterClient_PassNullEntity_ReturnBadRequest_ReturnStatusCode400()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Account.GetRoles()).Returns(It.IsAny<List<IdentityRole>>());
            _unitOfWorkMock.Setup(x => x.Account.CreateAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _unitOfWorkMock.Setup(x => x.Employee.Add(It.IsAny<Employee>())).Verifiable();

            //Act
            var result = await _sut.RegisterClient(null);

            //Assert

            var resultBadRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, resultBadRequest.StatusCode);
        }


    }
}

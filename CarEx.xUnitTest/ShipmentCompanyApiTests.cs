using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Model;
using CarEx.Utility;
using CarEx.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarEx.xUnitTest
{
   public class ShipmentCompanyApiTests
    {
        private readonly ShipmentCompanyController _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<ILogEngine> _loggerMock = new Mock<ILogEngine>();
        public ShipmentCompanyApiTests()
        {
            _sut = new  ShipmentCompanyController(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        private static IEnumerable<ShipmentCompany> Multiple()
        {
            return new List<ShipmentCompany>() { new ShipmentCompany {
                Id = 1,
                  ContactName = It.IsAny<string>(),
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),
            }, new ShipmentCompany{
                Id = 2,
                ContactName = It.IsAny<string>(),
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),
        },
         new ShipmentCompany{
                Id = 3,
                ContactName = It.IsAny<string>(),
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = It.IsAny<string>(),
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),
            }

            };
        }

        [Fact]
        public void Get_ShipmentCompanyById_ReturnShipmentCompany()
        {

            var id = 2;
            var companyName = It.IsAny<string>();
            ShipmentCompany cmpDto = new ShipmentCompany
            {
                ContactName = It.IsAny<string>(),
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Id = id,
                Name = companyName,
                Status = true
            };

            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Get(id)).Returns(Multiple().Where(u => u.Id == id).FirstOrDefault());
            //Act
            var result = _sut.Get(id);//.Result as OkObjectResult;

            //Assert
            var actionResult = Assert.IsType<ActionResult<ShipmentCompany>>(result);
            var actionValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(id, (actionValue.Value as ShipmentCompany).Id);
            // Assert.True(cmpDto.Equals(actionValue.Value as ShipmentCompany));
            // Assert.True(Multiple().Where(u => u.Id == id).FirstOrDefault().Equals(actionValue.Value as ShipmentCompany));
        }

        [Fact]
        public void Get_ShipmentCompanyById_WhenThereIsNoRecord_ReturnNotFound()
        {
            //Arrange
            var id = 34;

            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Get(It.IsAny<int>())).Returns(() => null);
            //Act
            var result = _sut.Get(id).Result;
            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShipmentCompanyPassZeroId_ReturnBadRequest()
        {
            //Arrange
            int id = 0;
            ShipmentCompany cmpDto = new ShipmentCompany
            {
                ContactName = It.IsAny<string>(),
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Id = id,
                Name = It.IsAny<string>(),
                Status = true
            };

            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Get(id)).Returns(cmpDto);
            //Act
            var result = _sut.Get(0);
            //Assert
            var actionResult = Assert.IsType<ActionResult<ShipmentCompany>>(result);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetAll_ReturnsAllShipmentCompanies_AsList()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.GetAll(null, null, null)).Returns(Multiple());
            //Act
            var result = _sut.GetAll();
            //Assert
            var model = Assert.IsAssignableFrom<IEnumerable<ShipmentCompany>>(result);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void Post_AddNewShipmentCompany_returnAddedShipmentCompany()
        {
            //Arrange
            ShipmentCompany company = new ShipmentCompany
            {
                ContactName = "Kamil Zeynalli",
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = "Zamir",
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),
                
            };
            //Act
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Add(company));
            var result = _sut.Post(company);

            //Assert
            var actionResult = Assert.IsType<ActionResult<ShipmentCompany>>(result);
            var actionValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var addedValue = (ShipmentCompany)actionValue.Value;
            Assert.Equal(company.ContactName, addedValue.ContactName);
            Assert.Equal("Zamir", addedValue.Name);
            Assert.True(company.Equals(addedValue));
        }

        [Fact]
        public void Post_AddNullValue_ReutnBadRequestType_ReturnStatusCode400()
        {
            //Arrange
            ShipmentCompany company = new ShipmentCompany
            {
                ContactName = "Kamil Zeynalli",
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = "Zamir",
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),

            };
            //Act
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Add(company));
            var result = _sut.Post(null);

            //Assert
            var actionResult = Assert.IsType<ActionResult<ShipmentCompany>>(result);
            var actionValue = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(400, actionValue.StatusCode);
        }


        [Fact]
        public void Update_ShipmentCompany_ReturnSuccessString_ReturnStatusCode200()
        {
            //Arrange
            ShipmentCompany company = new ShipmentCompany
            {
                ContactName = "Kamil Zeynalli",
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = "Zamir",
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),

            };
            //Act
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Update(company));

            var result = _sut.Update(company);
            //Assert

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Success", (actionResult.Value as string));
            Assert.Equal(200, actionResult.StatusCode);

        }


        [Fact]
        public void Update_SendNullValue_ReturnBadRequest_ReturnStatusCode400()
        {
            //Arrange
            ShipmentCompany company = new ShipmentCompany
            {
                ContactName = "Kamil Zeynalli",
                CreatedOn = It.IsAny<DateTime>(),
                EmployeeId = It.IsAny<int>(),
                Name = "Zamir",
                ResponsibleName = It.IsAny<string>(),
                Status = It.IsAny<Boolean>(),

            };
            //Act
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Update(company));

            var result = _sut.Update(null);
            //Assert

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
          //  var actionValue = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            
            Assert.Equal(400, actionResult.StatusCode);

        }

        [Fact]
        public void Delete_ShipmenCompanyIdByDelete_ReturnSuccessMessage_ReturnStatusCode200()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Remove(It.IsAny<int>())).Verifiable();
            //Act
            var result = _sut.Delete(3);
            //Assert

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Success", actionResult.Value);
            Assert.Equal(200, actionResult.StatusCode);

        }

        [Fact]
        public void Delete_PassZeroId_ReturnBadRequest_ReturnStatusCode400()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.ShipmentCompany.Remove(It.IsAny<int>())).Verifiable();
            //Act
            var result = _sut.Delete(0);
            //Assert

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StaticMessages.BadRequestValueMessage, actionResult.Value);
            Assert.Equal(400, actionResult.StatusCode);

        }


    }
}

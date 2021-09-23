using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Log.Model;
using CarEx.Core.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarEx.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentCompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogEngine _logger;
        public ShipmentCompanyController(IUnitOfWork unitOfWork,ILogEngine logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_unitOfWork.ShipmentCompany.Get(id));
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(new { data = _unitOfWork.ShipmentCompany.GetAll() });

        [HttpPost]
        public IActionResult Post([FromBody] ShipmentCompany shipmentCompany) {
            try
            {
                if (shipmentCompany != null)
                {
                    _unitOfWork.ShipmentCompany.Add(shipmentCompany);
                    _unitOfWork.Save();
                    return Ok(shipmentCompany.Name);
                }
                return Ok("Model is null");
            } 
            catch (Exception ex)
            {
               return Ok(_logger.SendError(ex.Message));
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody] ShipmentCompany shipmentCompany)
        {
            try
            {
                if (shipmentCompany != null)
                {
                    _unitOfWork.ShipmentCompany.Update(shipmentCompany);
                    _unitOfWork.Save();
                    return Ok("Success");
                }
                return Ok("Fail - Model is null");
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    _unitOfWork.ShipmentCompany.Remove(id);
                    _unitOfWork.Save();
                    return Ok("Success");
                }
                return Ok("Fail - Id is 0");
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarEx.Business.UnitOfWork;
using CarEx.Core.Dto;
using CarEx.Core.Log.Business;
using CarEx.Core.Log.Model;
using CarEx.Core.Model;
using CarEx.Utility;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentCompany))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ShipmentCompany> Get(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest(StaticMessages.BadRequestValueMessage);

                var shipmentCompany = _unitOfWork.ShipmentCompany.Get(id);
                if (shipmentCompany == null)
                    return NotFound();
                return Ok(shipmentCompany);
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentCompany))]
        public IEnumerable<ShipmentCompany> GetAll() => _unitOfWork.ShipmentCompany.GetAll();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShipmentCompany))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ShipmentCompany> Post([FromBody] ShipmentCompany shipmentCompany) {
            try
            {
                if (shipmentCompany == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);
              
                    _unitOfWork.ShipmentCompany.Add(shipmentCompany);
                    _unitOfWork.Save();
                    return Ok(shipmentCompany);
            } 
            catch (Exception ex)
            {
               return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] ShipmentCompany shipmentCompany)
        {
            try
            {
                if (shipmentCompany == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);
               
                _unitOfWork.ShipmentCompany.Update(shipmentCompany);
                    _unitOfWork.Save();
                    return Ok("Success");
               
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest(StaticMessages.BadRequestValueMessage);
              
                    _unitOfWork.ShipmentCompany.Remove(id);
                    _unitOfWork.Save();
                    return Ok("Success");
               
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }
    }
}
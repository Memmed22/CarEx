using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarEx.Business.UnitOfWork;
using CarEx.Core.Log.Business;
using CarEx.Core.Model;
using CarEx.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarEx.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogEngine _logger;
        public ShipmentController(IUnitOfWork unitOfWork, ILogEngine logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shipment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Shipment> Get(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest(StaticMessages.BadRequestValueMessage);

                var shipment = _unitOfWork.Shipment.Get(id);
                if (shipment == null)
                    return NotFound();
                return Ok(shipment);
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shipment))]
        public IEnumerable<Shipment> GetAll() => _unitOfWork.Shipment.GetAll();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shipment))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Shipment> Post([FromBody] Shipment shipment)
        {
            try
            {
                if (shipment == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);

                _unitOfWork.Shipment.Add(shipment);
                _unitOfWork.Save();
                return Ok(shipment);
            }
            catch (Exception ex)
            {
                return Ok(_logger.SendError(ex.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] Shipment shipment)
        {
            try
            {
                if (shipment == null)
                    return BadRequest(StaticMessages.BadRequestEntityMessage);

                _unitOfWork.Shipment.Update(shipment);
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

                _unitOfWork.Shipment.Remove(id);
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

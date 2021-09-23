using CarEx.Business.Repository;
using CarEx.Business.Services.Abstract;
using CarEx.Core.Model;
using CarEx.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Business.Services.Concrete
{
   public class ShipmentCompanyService : Repository<ShipmentCompany>, IShipmentCompanyService
    {
        private readonly CarExDbContext _context;
        public ShipmentCompanyService(CarExDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

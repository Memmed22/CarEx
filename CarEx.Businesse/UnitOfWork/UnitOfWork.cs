using CarEx.Business.Services.Abstract;
using CarEx.Business.Services.Concrete;
using CarEx.Core.Model;
using CarEx.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly CarExDbContext _context;
             
        public IEmployeeService Employee { get; private set; }

        public IUserService User { get; private set; }

        public IShipmentCompanyService ShipmentCompany { get; private set; }

        public IShipmentService Shipment { get; private set; }

        public IParcelService Parcel{ get; private set; }

        public IPackageService Package { get; private set; }

        public UnitOfWork(CarExDbContext context)
        {
            _context = context;
            Employee = new EmployeeService(context);
            User = new UserService(context);
            ShipmentCompany = new ShipmentCompanyService(context);
            Shipment = new ShipmentService(context);
            Parcel = new ParcelService(context);
            Package = new PackageService(context);

        }

        public void Save() {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

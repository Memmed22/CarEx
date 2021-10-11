using CarEx.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Business.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeService Employee { get; }
        public IClientService User { get; }

        public IShipmentCompanyService ShipmentCompany { get; }
        public IShipmentService Shipment { get; }
        public IParcelService Parcel { get; }
        public IPackageService Package { get; }

        public IAccountService Account { get;  }

        public void Save();
    }
}

using CarEx.Business.Services.Abstract;
using CarEx.Business.Services.Concrete;
using CarEx.Core.Model;
using CarEx.Data.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Business.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarExDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IEmployeeService Employee { get; private set; }

        public IClientService User { get; private set; }

        public IShipmentCompanyService ShipmentCompany { get; private set; }

        public IShipmentService Shipment { get; private set; }

        public IParcelService Parcel{ get; private set; }

        public IPackageService Package { get; private set; }

        public IAccountService Account { get; private set; }

        public UnitOfWork(CarExDbContext context, UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            Employee = new EmployeeService(context);
            User = new ClientService(context);
            ShipmentCompany = new ShipmentCompanyService(context);
            Shipment = new ShipmentService(context);
            Parcel = new ParcelService(context);
            Package = new PackageService(context);
            Account = new AccountService(_userManager, _roleManager, _context);
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

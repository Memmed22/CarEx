using CarEx.Business.Repository;
using CarEx.Business.Services.Abstract;
using CarEx.Core.Model;
using CarEx.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CarEx.Business.Services.Concrete
{
   public class EmployeeService : Repository<Employee>, IEmployeeService
    {
        public readonly CarExDbContext _context;
        public EmployeeService(CarExDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

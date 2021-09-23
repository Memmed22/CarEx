using CarEx.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Core.ViewModel
{
   public class AccountViewModel : IViewModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TelNo { get; set; }
        public string PersonId { get; set; }
        public string Adress { get; set; }
        public string AccountType { get; set; }

        public string ClientCode { get; set; }
        public string Photo { get; set; }

    }
}

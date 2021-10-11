using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Core.Dto
{
    public class ShipmentCompanyDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactName { get; set; }

        public string TelNo2 { get; set; }

        public string ResponsibleName { get; set; }

        public string ResponsibleTelNo { get; set; }

        public string ShipmentType { get; set; }

        public bool Status { get; set; }

    }
}

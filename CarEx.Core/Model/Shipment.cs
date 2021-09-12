using CarEx.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class Shipment : EntityUtility,IModelEntity
    {
     
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set ; }
   
        [Required]
        [Display(Name="Shipment Code")]
        public string ShipmentCode { get; set; }

   
        public int ShipmentCompanyId { get; set; }
        [ForeignKey("ShipmentCompanyId")]
        public virtual ShipmentCompany ShipmentCompany { get; set; }

        [Display(Name="Carrier Plate No")]
        [StringLength(20)]
        public string PlateNo { get; set; }

        [Display(Name = "Dirvier Full Name")]
        [StringLength(50)]
        public string DriverFullName { get; set; }

        [Display(Name = "Driver TelNo")]
        [StringLength(40)]
        public string DriverTelNo { get; set; }

        public DateTime EstimatedDeliverDate { get; set; }

        [Display(Name = "Gross Weight")]
        public double GrossWeight { get; set; }

        [StringLength(500)]
        [Display(Name="Comment")]
        public string Comment { get; set; }



        public DateTime CreatedOn { get ; set; }

        [NotMapped]
        public string GetCreatedDateString => GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get; set; }

        [NotMapped]
        public string GetUpdateDateString =>GetShortDateTimeFormat (UpdatedOn);


        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]

        public virtual Employee Employee { get; }

    }
}

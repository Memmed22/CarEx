using CarEx.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class ShipmentCompany : EntityUtility,IModelEntity
    {     
        public int Id { get ; set ; }

        [Required]
        [Display(Name="Company Name")]
        [StringLength(40)]
        public string Name { get; set ; }

        [Display(Name="Contact Name")]
        [StringLength(50)]
        public string ContactName { get; set; }
       
        [Display(Name = "Tel No")]
        [StringLength(40)]
        public string TelNo2 { get; set; }

        [Display(Name = "Responsible Name")]
        [StringLength(50)]
        public string ResponsibleName { get; set; }

        [Display(Name="Responsible Tel No")]
        [StringLength(40)]
        public string  ResponsibleTelNo { get; set; }

        [Display(Name = "Shipment Type")]
        public string ShipmentType { get; set; }

        public bool Status { get; set; }



        public DateTime CreatedOn { get; set ; }
        [NotMapped]
        public string GetCreatedDateString => GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get ; set; }
        [NotMapped]
        public string GetUpdateDateString => GetShortDateTimeFormat(UpdatedOn);


        public int EmployeeId { get; set ; }

        [ForeignKey("EmployeeId")]
        public virtual  Employee Employee { get; set; }
    }
}

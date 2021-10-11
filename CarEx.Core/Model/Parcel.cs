using CarEx.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class Parcel : EntityUtility, IModelEntity
    {  
        public int Id { get ; set ; }
        [DisplayName("Parcel Name")]
        [StringLength(40)]
        public string Name { get; set; }

        public DateTime CreatedOn { get ; set; }
        
        [NotMapped]
        public string GetCreatedDateString => GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public string GetUpdateDateString => GetShortDateTimeFormat(UpdatedOn);

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }


        public int ShipmentId { get; set; }
        [ForeignKey("ShipmentId")]
        public virtual Shipment Shipment { get; set; }

        public string Status { get; set; }
        public string Invoice { get; set; }
        [Required]
        public double Weight { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01,10000, ErrorMessage ="Fee must be bigger than 0")]
        public double Fee { get; set; }
        public string Goods { get; set; }
        public string Comment { get; set; }


    }
}

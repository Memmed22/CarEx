using CarEx.Core.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class Package : EntityUtility, IModelEntity
    {
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public string Invoice { get; set; }

        public string Status { get; set; }
        [Required]
        public double Weight { get; set; }

        [DataType(DataType.Currency)]
        public double Fee { get; set; }
        [Required]
        public string Goods { get; set; }
        public string Comment { get; set; }
        [Required]
        public double InvoicePrice { get; set; }
        [Required]
        public string Supplier { get; set; }

        public int ParcelId { get; set; }
        [ForeignKey("ParcelId")]
        public virtual Parcel Parcel { get; set; }


        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public DateTime CreatedOn { get; set ; }
        [NotMapped]
        public string GetCreatedDateString => base.GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public string GetUpdateDateString => base.GetShortDateTimeFormat(UpdatedOn);
    }
}

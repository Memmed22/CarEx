using CarEx.Core.Model.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class User : IEntityUtility, IEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }



        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get => Name + " " + Surname; }

        public string AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [Required]
        public string PersonId { get; set; }

        public string ClientCode { get; set; }
        [StringLength(300)]
        public string Adress { get; set; }
        [StringLength(40)]
        [Display(Name="Tel No")]
        public string TelNo { get; set; }
        [StringLength(40)]
        public string Email { get; set; }

        public double SpendingLimit { get; set; }
        public string Photo { get; set; }

        public DateTime CreatedOn { get ; set ; }
        [NotMapped]
        public string GetCreatedDateString =>GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public string GetUpdateDateString => GetShortDateTimeFormat(UpdatedOn);

        EntityUtility entityUtility = new EntityUtility();
        public string GetShortDateTimeFormat(DateTime dateTime) => entityUtility.GetShortDateTimeFormat(dateTime);
    }
}

using CarEx.Core.Model.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class Employee :  IEntityUtility, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get => Name + " " + Surname; }

        [Display(Name = "Tel No")]
        public string TelNo { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string  Photo { get; set; }

        public string AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }


        public DateTime CreatedOn { get; set ; }

        [NotMapped]
        public string GetCreatedDateString => GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public string GetUpdateDateString => GetShortDateTimeFormat(UpdatedOn);

        readonly EntityUtility abstractEntity = new EntityUtility();
        public string GetShortDateTimeFormat(DateTime dateTime)
        {
            return abstractEntity.GetShortDateTimeFormat(dateTime);
        }
    }
}

using CarEx.Core.Model.Abstract;
using CarEx.Utility;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model
{
    public class Account : IdentityUser, IEntityUtility, IEntity
    {
        public string Password { get; set; }
        public string AccountType { get; set; }

        public DateTime CreatedOn { get; set ; }
        [NotMapped]
        public string GetCreatedDateString => GetShortDateTimeFormat(CreatedOn);

        public DateTime UpdatedOn { get ; set ; }
        [NotMapped]
        public string GetUpdateDateString => GetShortDateTimeFormat(UpdatedOn);
        [NotMapped]

        EntityUtility entityUtility = new EntityUtility();
        public string GetShortDateTimeFormat(DateTime dateTime) => entityUtility.GetShortDateTimeFormat(dateTime);
    }
}

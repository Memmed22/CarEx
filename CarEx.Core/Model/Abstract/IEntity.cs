using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model.Abstract
{
   public interface IEntity 
    {
       
        public DateTime CreatedOn { get; set; }
        public string GetCreatedDateString { get;  }
        public DateTime UpdatedOn { get; set; }
        public string GetUpdateDateString { get; }

    }
}

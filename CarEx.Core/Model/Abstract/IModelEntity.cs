using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarEx.Core.Model.Abstract
{
    public interface IModelEntity : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get => null; }

    }
}

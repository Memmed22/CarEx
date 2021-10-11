using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Core.Model.Abstract
{
    public class EntityUtility : IEntityUtility
    {
        public virtual string GetShortDateTimeFormat(DateTime dateTime) => dateTime.ToShortDateString() + " - " + dateTime.ToShortTimeString();
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CarEx.Utility
{
    public enum EnumAccountType
    {
        [EnumMember(Value = "Employee")]
        EMPLOYEE,
        [EnumMember(Value = "User")]
        USER
    }
}

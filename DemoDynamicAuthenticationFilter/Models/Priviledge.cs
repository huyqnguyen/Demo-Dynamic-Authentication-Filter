using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDynamicAuthenticationFilter.Models
{
    [Flags]
    public enum Priviledge
    {
        None = 0,
        Read = 1,
        Edit = 2,
    }
}
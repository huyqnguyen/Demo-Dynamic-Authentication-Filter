using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDynamicAuthenticationFilter
{
    public class ViewNameAttribute : Attribute
    {

        public ViewNameAttribute(string[] viewName)
        {
            ViewName = viewName;
        }
        public ViewNameAttribute(string viewName)
        {
            ViewName = new[] { viewName };
        }
        public string[] ViewName { get; set; }
    }
}
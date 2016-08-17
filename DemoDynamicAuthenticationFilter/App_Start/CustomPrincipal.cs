using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace DemoDynamicAuthenticationFilter.App_Start
{
    public class SerializablePrincipal
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<FilterElement> Filters { get; set; }
    }

    public class CustomPrincipal : SerializablePrincipal, IPrincipal
    {
        public IIdentity Identity { get; private set; }
        // Used by AuthorizeAttribute
        public bool IsInRole(string role)
        {
            //return Identity != null && Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
            return Identity != null && Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(role) && String.Equals(this.Role, role, StringComparison.CurrentCultureIgnoreCase);
        }

        public CustomPrincipal(string Name)
        {
            this.Identity = new GenericIdentity(Name);
        }
    }
    public class FilterElement
    {
        public string Name { get; set; }
        public string Allow { get; set; }
        public string Deny { get; set; }
    }
}
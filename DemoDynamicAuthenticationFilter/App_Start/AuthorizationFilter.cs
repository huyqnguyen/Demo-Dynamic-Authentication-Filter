using DemoDynamicAuthenticationFilter.Utilize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoDynamicAuthenticationFilter.App_Start
{
    

    public class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public int Accessibilities { get; set; }

        public AuthorizationFilterAttribute(int accessibilities)
        {
            Accessibilities = accessibilities;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var principal = SerializerHelper.JavaScriptDeserialize<SerializablePrincipal>(authTicket.UserData);
                var user = new CustomPrincipal(authTicket.Name)
                {
                    Id = principal.Id,
                    IdRole = principal.IdRole,
                    Name = principal.Name,
                    Role = principal.Role,
                    Filters = principal.Filters
                };
                filterContext.HttpContext.User = user;

                var attrAuthens = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AuthorizationFilterAttribute), false).FirstOrDefault();
                var attrViewNameActions = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewNameAttribute), false).FirstOrDefault();
                var attrViewNameControllers = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ViewNameAttribute), false).FirstOrDefault();
                if (attrAuthens != null)
                {
                    if (attrViewNameActions == null && attrViewNameControllers == null)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                    {
                        var access = ((AuthorizationFilterAttribute)attrAuthens).Accessibilities;
                        var viewName = attrViewNameActions != null
                            ? ((ViewNameAttribute)attrViewNameActions).ViewName[0]
                            : ((ViewNameAttribute)attrViewNameControllers).ViewName[0];

                        var pageAccessManager = PageAccessManager.GetInstance();
                        var priviledge = pageAccessManager.GetUserPrivilege(user, viewName);

                        if ((priviledge & access) != access)
                        {
                            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                            {
                                filterContext.Result = new JsonResult { Data = new { Status = 404 } };
                            }
                            else
                            {
                                filterContext.Result = new RedirectResult("/Home");
                            }
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}
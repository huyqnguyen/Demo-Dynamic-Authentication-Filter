using DemoDynamicAuthenticationFilter.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;

namespace DemoDynamicAuthenticationFilter.Utilize
{
    public class PageAccessManager
    {
        private static PageAccessManager _pageAccessManager;
        public static PageAccessManager GetInstance()
        {
            if (_pageAccessManager == null) _pageAccessManager = new PageAccessManager();
            return _pageAccessManager;
        }
        public int GetUserPrivilege(IPrincipal user, string viewName)
        {
            try
            {
                if (user != null && user.GetType() == typeof(CustomPrincipal))
                {
                    var idRole = (user as CustomPrincipal).IdRole;
                    using (var db = new TestAuthorizationEntities())
                    {
                        var result = (from tv in db.TViews
                                      join ta in db.TViewAccesses on tv.Id equals ta.IdView
                                      where ta.IdRole == idRole && tv.Name == viewName
                                      select new
                                      {
                                          AccessRight = ta.AccessRight
                                      }).FirstOrDefault();
                        if (result != null)
                        {
                            return result.AccessRight;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        internal Dictionary<string, List<string>> GetUserFilter()
        {
            throw new NotImplementedException();
        }

        internal string GetUserRoles(IPrincipal user)
        {
            try
            {
                if (user != null && user.GetType() == typeof(CustomPrincipal))
                {
                    var context = new TestAuthorizationEntities();

                    var idRole = (user as CustomPrincipal).IdRole;
                    var result = (from tv in context.TViews
                                  join ta in context.TViewAccesses on tv.Id equals ta.IdView
                                  where ta.IdRole == idRole
                                  select new
                                  {
                                      ViewName = tv.Name,
                                      Privilege = ta.AccessRight
                                  });
                    return new JavaScriptSerializer().Serialize(result.ToList());
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
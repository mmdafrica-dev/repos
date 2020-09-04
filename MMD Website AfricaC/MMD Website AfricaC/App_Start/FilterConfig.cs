using System.Web;
using System.Web.Mvc;

namespace MMD_Website_AfricaC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

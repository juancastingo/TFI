using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TFITest4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
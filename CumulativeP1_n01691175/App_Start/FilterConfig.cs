using System.Web;
using System.Web.Mvc;

namespace CumulativeP1_n01691175
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

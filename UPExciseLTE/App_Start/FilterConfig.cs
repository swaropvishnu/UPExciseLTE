using System.Web;
using System.Web.Mvc;
using UPExciseLTE.Helper;

namespace UPExciseLTE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AutoPopulateSourceCodeAttribute());
            //filters.Add(new CustomErrorHandler());
        }
    }
}

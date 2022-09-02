using System.Web;
using System.Web.Mvc;

namespace Nhom5_ShopBanDoTrangSuc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
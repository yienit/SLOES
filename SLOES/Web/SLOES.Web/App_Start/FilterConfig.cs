using System.Web.Mvc;
using SLOES.Web.Filters;

namespace SLOES.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UTF8EncodingFilter());
        }
    }
}
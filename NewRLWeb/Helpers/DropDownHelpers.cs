using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace NewRLWeb.Helpers
{
    public static class DropDownHelpers
    {
        public static MvcHtmlString DropDownForAddresss(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<select id=\"address\"><option>地址</option>");
            foreach (string ad in Province.Provinces)
            {
                sb.Append("<option value=\"");
                sb.Append(ad);
                sb.Append("\">");
                sb.Append(ad);
                sb.Append("</option>");
            }
            sb.Append("</select>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
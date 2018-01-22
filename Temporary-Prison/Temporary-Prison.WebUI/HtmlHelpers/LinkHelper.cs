using System.Web.Mvc;
using System.Web.Routing;

namespace Temporary_Prison.HtmlHelpers
{
    public static class LinkHelper
    {
        public static MvcHtmlString Link(this HtmlHelper helper, string url, string linkText, object htmlAttributes)
        {
            var link = new TagBuilder("a");
            link.MergeAttribute("href", url);
            link.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            link.InnerHtml = linkText;

            return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
        }
    }
}
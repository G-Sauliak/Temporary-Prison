using System.Web.Mvc;
using System.Web.Routing;

namespace Temporary_Prison.HtmlHelpers
{
    public static class PhotoHelper
    {
        public static MvcHtmlString Photo(this HtmlHelper helper, string url, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", url);
            if (string.IsNullOrEmpty(url))
            {
                builder.MergeAttribute("style", "display:none");
            }
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

    }
}
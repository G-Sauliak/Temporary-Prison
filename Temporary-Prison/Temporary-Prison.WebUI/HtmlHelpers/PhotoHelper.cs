using System.Configuration;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class PhotoHelper
    {
        public static MvcHtmlString Avatar(this HtmlHelper helper, string url, object htmlAttributes)
        {
            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", url);
            builder.MergeAttribute("Width", ConfigurationManager.AppSettings["PrisonerAvatarWidth"]);
            builder.MergeAttribute("Height", ConfigurationManager.AppSettings["PrisonerAvatarHeight"]);

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
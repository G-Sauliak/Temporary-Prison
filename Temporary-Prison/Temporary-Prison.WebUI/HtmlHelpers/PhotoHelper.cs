﻿using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class PhotoHelper
    {
        public static MvcHtmlString Photo(this HtmlHelper helper, string url, object htmlAttributes)
        {
            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", url);

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
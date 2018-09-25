using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RetailBay.WebAdministration.TagHelpers
{
    [HtmlTargetElement("paging", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PagingTagHelper : TagHelper
    {
        #region Fields

        private readonly IUrlHelper _urlHelper;

        #endregion Fields

        #region Constructors

        public PagingTagHelper(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        #endregion Constructors

        #region Properties

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected HttpRequest Request => ViewContext.HttpContext.Request;

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("page-number")]
        public int PageNumber { get; set; }

        #endregion Properties

        #region Methods

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "PagingTagHelper";

            var action = ViewContext.RouteData.Values["action"].ToString();
            var urlTemplateBuilder = new StringBuilder();
            urlTemplateBuilder.Append(WebUtility.UrlDecode(_urlHelper.Action(action, new { pageNumber = "{0}" })));

            // Preserve rest of the query parameters.
            foreach (var key in Request.Query.Keys)
            {
                if (key == "pageNumber")
                    continue;

                urlTemplateBuilder.Append("&");
                urlTemplateBuilder.Append(key);
                urlTemplateBuilder.Append("=");
                urlTemplateBuilder.Append(Request.Query[key]);
            }

            var urlTemplate = urlTemplateBuilder.ToString();
            var startIndex = Math.Max(PageNumber - 5, 1);
            var finishIndex = Math.Min(PageNumber + 5, PageCount);

            var disablePrevius = PageNumber <= 1;
            var disableNext = PageNumber >= PageCount;

            output.Content.AppendHtml("<ul class=\"pagination\">");

            // Previous page link.
            AddPageLink(output, string.Format(urlTemplate, 1), "&laquo;", disablePrevius);

            for (var i = startIndex; i <= finishIndex; i++)
            {
                if (i == PageNumber)
                    AddCurrentPageLink(output, i);
                else
                    AddPageLink(output, string.Format(urlTemplate, i), i.ToString());
            }

            // Next page link.
            AddPageLink(output, string.Format(urlTemplate, PageCount), "&raquo;", disableNext);
            output.Content.AppendHtml("</ul>");
        }

        private void AddPageLink(TagHelperOutput output, string url, string text, bool disable = false)
        {
            if (disable)
                output.Content.AppendHtml($"<li class=\"disabled\"><a href=\"javascript:void(0)");
            else
            {
                output.Content.AppendHtml($"<li><a href=\"");
                output.Content.AppendHtml(url);
            }

            output.Content.AppendHtml("\">");
            output.Content.AppendHtml(text);
            output.Content.AppendHtml("</a>");
            output.Content.AppendHtml("</li>");
        }

        private void AddCurrentPageLink(TagHelperOutput output, int page)
        {
            output.Content.AppendHtml("<li class=\"active\">");
            output.Content.AppendHtml("<span>");
            output.Content.AppendHtml(page.ToString());
            output.Content.AppendHtml("</span>");
            output.Content.AppendHtml("</li>");
        }

        #endregion Methods
    }
}

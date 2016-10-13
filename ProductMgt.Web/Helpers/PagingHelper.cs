using ProductMgt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfoViewModel pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            if (pagingInfo.CurrentPage > 1)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(1));
                tag.InnerHtml = "首页";
                result.AppendFormat("<li>{0}</li>", tag.ToString());

                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tag.InnerHtml = "上一页";
                result.AppendFormat("<li>{0}</li>", tag.ToString());
            }

            for (int i = pagingInfo.StartPage; i <= pagingInfo.EndPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                    result.AppendFormat("<li class=\"active\">{0}</li>", tag.ToString());
                else
                    result.AppendFormat("<li>{0}</li>", tag.ToString());
            }

            if (pagingInfo.CurrentPage < pagingInfo.TotalPages)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tag.InnerHtml = "下一页";
                result.AppendFormat("<li>{0}</li>", tag.ToString());

                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
                tag.InnerHtml = "末页";
                result.AppendFormat("<li>{0}</li>", tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ESG.DailyDigest.Infrastructure
{
    public static class Extensions
    {
        public static HtmlNode SelectNode(this HtmlNode context, string selector)
        {
            return context.SelectSingleNode(selector);
        }

        public static string SelectNodeValue(this HtmlNode context, string selector)
        {
            HtmlNode node = SelectNode(context, selector);
            return !ReferenceEquals(node, null) ? node.InnerText : string.Empty;
        }
    }
}

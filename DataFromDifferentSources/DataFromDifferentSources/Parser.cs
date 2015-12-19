using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace DataFromWeb
{
   public class Parser
    {


        public static DateTime ConvertToDateTime(string datetime)
        {
            //11:55:10 18.12.2015 convert to 18/12/2015 11:55.10

            datetime = String.Format(
                "{2} {0}.{1}",
                datetime.Substring(9, 10).Replace(".", "/"),
                datetime.Substring(0, 6),
                datetime.Substring(6, 2));

            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            return Convert.ToDateTime(datetime, culture);

        }

        static string GetWellFormedHtmlTable(string htmlTable)
        {
            // here arises the problem of making sure that the html table has a correct syntax
            // in order for it to be parsed as an XElement
            htmlTable = "<table id=\"" + htmlTable;
            string pattern1 = "<td[a-zA-Z0-9= \"%_#]*>";
            string pattern2 = "<tr[a-zA-Z0-9= \"%_#]*>";
            string pattern3 = "<table[a-zA-Z0-9= \"%_#]*>";
            string pattern4 = @"<(img|a|b)[^>]*>(?<content>[^<]*)";
            string pattern5 = @"</(a|b)>";
            string wellformedHtmlTable = (new Regex(pattern1)).Replace(htmlTable, "<td>");
            wellformedHtmlTable = (new Regex(pattern2)).Replace(wellformedHtmlTable, "<tr>");
            wellformedHtmlTable = (new Regex(pattern3)).Replace(wellformedHtmlTable, "<table>");
            wellformedHtmlTable = (new Regex(pattern4)).Replace(wellformedHtmlTable, "");
            wellformedHtmlTable = (new Regex(pattern5)).Replace(wellformedHtmlTable, "").Replace("&nbsp;", " ");
            return wellformedHtmlTable;
        }

        static string GetRawHtmlTable(string link, string startKeyword, string endKeyword)
        {
            // reading from a web page only a table
            string html = new WebClient().DownloadString(link).ToLower();
            int startIndex = html.IndexOf(startKeyword);
            int endIndex = html.IndexOf(endKeyword, startIndex);
            return html.Substring(startIndex, endIndex - startIndex + endKeyword.Length).ToLower();
        }

        public static string GetHtmlTable(string link, string startKeyword, string endKeyword)
        {
            string rowHtmlTable = Parser.GetRawHtmlTable(link, startKeyword, endKeyword);
            return Parser.GetWellFormedHtmlTable(rowHtmlTable);
        }
    }
}

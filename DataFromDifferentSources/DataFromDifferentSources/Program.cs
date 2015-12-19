using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
//using HtmlAgilityPack;

namespace DataFromWeb
{
    class Program
    {
        static void Main(string[] args)
        {

            string startKeyword = Configuration.ReadFromAppSettings("START_KEYWORD");
            string endKeyword = Configuration.ReadFromAppSettings("END_KEYWORD");
            string website = Configuration.ReadFromAppSettings("URL"); 

            string htmlTable = Parser.GetHtmlTable(website, startKeyword, endKeyword);
            XElement table = XElement.Parse(htmlTable);

            var rows = table.Elements()
                             .Select(tr => tr.Elements().Select(td => td.Value)
                             .ToArray())
                             .ToList();

            DbManager.InsertData(rows);

        }

     



     



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataFromWeb
{
    public class Configuration
    {


        public static string ReadFromAppSettings(string p)
        {
            string value = "";

            try
            {
                switch (p)
                {
                    case "URL":
                        value = ConfigurationManager.AppSettings["url"];
                        break;
                    case "START_KEYWORD":
                        value = ConfigurationManager.AppSettings["tableId"];
                        if (String.IsNullOrEmpty(value))
                            value = ConfigurationManager.AppSettings["cssClass"];
                        break;

                    case "END_KEYWORD":
                        value = "</table>";
                        break;

                    case "DB_CONNECTION":
                        value = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent(ex.Message);
            }
            return value;

        }
    }
}

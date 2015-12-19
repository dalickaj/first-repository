using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DataFromWeb
{
    public class DbManager
    {

        public static void InsertData(List<string[]> rows)
        {

            string dateStr = "";
            DateTime dateTime = new DateTime();
            foreach (string[] row in rows)
            {
                try
                {
                    // first row contains the date
                    if (rows.IndexOf(row) == 0)
                    {
                        dateStr = row[0];
                        dateTime = Parser.ConvertToDateTime(dateStr);
                        continue;
                    }

                    // second row doesn't contain any useful information
                    if (rows.IndexOf(row) == 1)
                        continue;

                    // starting from the third row
                    Exchange exch = new Exchange();
                    exch.CurrencyCode = row[1];
                    exch.ExchangeRate = Convert.ToDecimal(row[2]);
                    exch.Date = dateTime;
                    InsertSingleRow(exch);
                }
                catch (Exception ex)
                {

                    Logger.LogEvent(ex.Message);
                }

            }

        }

        static void InsertSingleRow(Exchange exchange)
        {

            string connectionString = Configuration.ReadFromAppSettings("DB_CONNECTION");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"IF NOT EXISTS( SELECT ExchangeId FROM CurrencyExchange 
                                    where CurrencyCode = @currencyCode and DateExchange = @dateExchange) 
                                    INSERT INTO CurrencyExchange (CurrencyCode,ExchangeRate, DateExchange) 
                                    VALUES (@currencyCode,@exchangeRate, @dateExchange)";

                    cmd.Parameters.Add(new SqlParameter("CurrencyCode", exchange.CurrencyCode));
                    cmd.Parameters.Add(new SqlParameter("exchangeRate", exchange.ExchangeRate));
                    cmd.Parameters.Add(new SqlParameter("dateExchange", exchange.Date));

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Logger.LogEvent(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent(ex.Message);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFromWeb
{
    public class Exchange
    {

        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime Date { get; set; }
    }
}

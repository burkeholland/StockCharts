using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockCharts.Models {
    public class QuoteViewModel {
        public string Date { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
    }
}
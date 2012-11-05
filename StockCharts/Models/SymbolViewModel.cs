using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockCharts.Models {
    public class SymbolViewModel {
        public long SymbolId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Models.QuoteViewModel> Quotes { get; set; }
    }
}
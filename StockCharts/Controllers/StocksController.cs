using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCharts.Controllers
{
    public class StocksController : Controller
    {
        readonly Data.StocksEntities _entities = new Data.StocksEntities();

        public JsonResult Quotes() {

            // define upper and lower limits for the data (10 yrs)
            var lower = DateTime.Parse("11/01/2002");
            var upper = DateTime.Parse("11/01/2012");

            var quotes = _entities.Quotes
                .Where(q => q.Date >= lower && q.Date <= upper && q.Symbol.Name == "MSFT")
                .OrderBy(q => q.Symbol.Name).ThenBy(q => q.Date)
                .AsEnumerable()
                .Select(q => new Models.QuoteViewModel {
                    Symbol = q.Symbol.Name,
                    Open = q.Open,
                    Close = q.Close,
                    High = q.High,
                    Low = q.Low,
                    Date = q.Date.ToString("yyyy/MM/dd"),
                    Volume = q.Volume
                })
                .ToList();

            return this.Json(quotes, JsonRequestBehavior.AllowGet);
        
        }
    }
}

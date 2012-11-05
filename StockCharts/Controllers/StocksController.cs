using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockCharts.Controllers
{
    public class StocksController : Controller
    {
        readonly Data.StocksEntities _entities = new Data.StocksEntities();

        [GET("symbols")]
        public JsonResult Symbols() {

            var symbols = _entities.Symbols
                           .AsEnumerable()
                          .Select(s => new Models.SymbolViewModel {
                              SymbolId = s.SymbolId,
                              Name = s.Name,
                              Description = s.Description,
                              Quotes = new List<Models.QuoteViewModel>()
                          })
                          .ToList();

            return this.Json(symbols, JsonRequestBehavior.AllowGet);
        }

        [GET("quotes/{symbol=MSFT}")]
        public JsonResult Quotes(string symbol) {

                // we have a LOT of stock data. define a begin point
            var limit = DateTime.Parse("01/01/2000");

                var quotes = _entities.Symbols
                             .AsEnumerable()
                             .Select(s => new Models.SymbolViewModel {
                                 SymbolId = s.SymbolId,
                                 Name = s.Name,
                                 Description = s.Description,
                                 Quotes = s.Quotes.OrderBy(q => q.Date)
                                 .Where(q => q.Date > limit && q.Date < DateTime.Now)
                                .AsEnumerable()
                                .Select(q => new Models.QuoteViewModel {
                                    Open = q.Open,
                                    Close = q.Close,
                                    High = q.High,
                                    Low = q.Low,
                                    Date = q.Date.ToString("yyyy/MM/dd"),
                                    Volume = q.Volume
                                })
                                .ToList()
                             })
                             .FirstOrDefault();

            return this.Json(quotes, JsonRequestBehavior.AllowGet);

        }
    }
}

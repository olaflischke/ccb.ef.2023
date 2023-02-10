using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradingDayDal
{
    // Entitätsklasse
    public class TradingDay
    {
        public TradingDay()
        {

        }

        public TradingDay(XElement tradingDayNode)
        {
            this.Date = Convert.ToDateTime(tradingDayNode.Attribute("time").Value);

            this.Currencies = tradingDayNode.Elements().Select(xe => new Currency()
                                                        {
                                                            Symbol = xe.Attribute("currency").Value,
                                                            EuroRate = Convert.ToDouble(xe.Attribute("rate").Value, NumberFormatInfo.InvariantInfo),
                                                            TradingDay = this
                                                        })
                                                        .ToList();
        }

        public DateTime Date { get; set; }
        public List<Currency> Currencies { get; set; }

        // PK für EF Core
        public int Id { get; set; }
    }
}

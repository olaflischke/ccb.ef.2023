using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradingDayDal
{
    public class Archive
    {
        public Archive(string url)
        {
            this.TradingDays=XDocument.Load(url).Root?.Descendants()
                                    .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Any(at => at.Name == "time"))
                                    .Select(xe => new TradingDay(xe))
                                    .ToList(); 

            //this.TradingDays = GetData(url);
        }

        private List<TradingDay>? GetData(string url)
        {
#nullable disable
            XDocument document = XDocument.Load(url);

            var days = document?.Root?.Descendants()
                                    .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Any(at => at.Name == "time"))
                                    .Select(xe => new TradingDay(xe) );

            List<TradingDay> tradingDays = days.ToList();
            return tradingDays;
#nullable enable
        }

        public List<TradingDay>? TradingDays { get; set; }
    }
}

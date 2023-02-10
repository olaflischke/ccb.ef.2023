using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TradingDayDal.Entities;

namespace TradingDayDal
{
    public class Archive
    {
        public Archive(string url)
        {
            this.TradingDays = GetData(url);

            SaveToDb();
        }

        public void SaveToDb()
        {
            DbContextOptions<TradingDayContext> options = new DbContextOptionsBuilder<TradingDayContext>()
                                            .UseSqlite("datasource=C:\\training\\cadcom\\Datenbanken\\TradingDayDB.db")
                                            .Options;

            using (TradingDayContext context = new TradingDayContext(options))
            {
                //// Nicht Produktiv-Code!
                //context.Database.EnsureDeleted();
                //// Nicht Produktiv-Code!
                //context.Database.EnsureCreated();

                // Produktiv-Code
                context.Database.Migrate();

                context.TradingDays.AddRange(this.TradingDays);

                context.SaveChanges();
            }

        }

        /// <summary>
        /// Liest die Daten einer GESMES-XML-Datei und gibt eine Liste von TradingDays zurück
        /// </summary>
        /// <param name="url">URL einer GESMES-XML-Datei</param>
        /// <returns>Liste von TradingDays</returns>
        private List<TradingDay> GetData(string url)
        {
            XDocument document = XDocument.Load(url);

            var days = document?.Root?.Descendants()
                                    .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Any(at => at.Name == "time"))
                                    .Select(xe => new TradingDay(xe));

            List<TradingDay> tradingDays = days.ToList();
            return tradingDays;
        }

        public List<TradingDay> TradingDays { get; set; }
    }
}

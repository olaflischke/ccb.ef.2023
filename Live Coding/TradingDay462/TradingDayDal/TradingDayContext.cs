using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingDayDal.Entities;

namespace TradingDayDal
{
    public class TradingDayContext : DbContext
    {
        public TradingDayContext()
        {

        }

        public TradingDayContext(DbContextOptions<TradingDayContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("datasource=C:\\training\\cadcom\\Datenbanken\\TradingDay3.db");
            }
        }

        public DbSet<TradingDay> TradingDays { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}

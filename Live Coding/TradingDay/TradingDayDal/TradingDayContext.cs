using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingDayDal
{
    public class TradingDayContext : DbContext
    {
        public TradingDayContext(DbContextOptions<TradingDayContext> options) : base(options) { }

        public DbSet<TradingDay> TradingDays { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}

using TradingDayDal;

namespace TRadingDayDalUnitTests
{
    public class Tests
    {
        string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";

        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void IsArchiveInitializing()
        {
            Archive archive=new Archive(url);

            Currency? first = archive.TradingDays.FirstOrDefault()?.Currencies.FirstOrDefault();

            Console.WriteLine($"{first.Symbol}: {first.EuroRate}");

            Assert.AreEqual(63, archive.TradingDays.Count());
        }
    }
}
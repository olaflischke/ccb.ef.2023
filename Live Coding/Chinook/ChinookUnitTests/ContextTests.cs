using ChinookDal.ChinookModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinookUnitTests
{
    public class Tests
    {
        ChinookContext context;

        [SetUp]
        public void Setup()
        {
            context = new ChinookContext();
        }

        [Test]
        public void IsNumberOfGenresCorrect()
        {
            var genres = context.Genres; //.ToList();

            Assert.AreEqual(25, genres.Count());
        }

        [Test]
        public void GetRockArtists()
        {
            var artistNames = context.Tracks.Where(tr => tr.Genre.Name == "Rock")
                                        .Select(tr => tr.Album.Artist.Name) // Tipp: Navigationseigenschaften!
                                        .Distinct(); 
                                                                             //.ToList();

            foreach (string item in artistNames)
            {
                Console.WriteLine(item);
            }
        }
    }
}
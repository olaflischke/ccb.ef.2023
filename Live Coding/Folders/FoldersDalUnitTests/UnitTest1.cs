using FoldersDal;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FoldersDalUnitTests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
            FolderContext context = ConfigureContext();

            if (context != null)
            {
                context.Database.EnsureDeleted(); // Datenbank löschen, wenn vorhanden
                context.Database.EnsureCreated(); // Datenbank anlegen, wenn nicht vorhanden

            }
        }

        private FolderContext ConfigureContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder<FolderContext>()
                                            .UseSqlite("datasource=C:\\ProgramData\\SQLite\\data\\folders.db")
                                            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                                            .Options;

            FolderContext context = new FolderContext(options);

            return context;
        }

        [Test]
        public void CanCreateFolders()
        {
            Folder root = new Folder() { Name = "root" };

            Folder sub1 = new Folder() { Name = "sub1" };
            Folder sub2 = new Folder() { Name = "sub2" };
            Folder sub3 = new Folder() { Name = "sub3" };

            root.Subfolders.Add(sub1);
            root.Subfolders.Add(sub2);
            root.Subfolders.Add(sub3);

            FolderContext context = ConfigureContext();

            if (context != null)
            {
                context.Folders.Add(root);
                context.SaveChanges();
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Context null - check config!");
            }
        }
    }
}
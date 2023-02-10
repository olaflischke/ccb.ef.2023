using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoldersDal
{
    public class FolderContext : DbContext
    {
        public FolderContext(DbContextOptions<FolderContext> options) : base(options)
        {

        }


        public DbSet<Folder> Folders { get; set; }


    }
}

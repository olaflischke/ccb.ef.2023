using System;
using System.Collections.Generic;

#nullable disable

namespace ChinookDal.ChinookModel
{
    public partial class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        public long GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace FoldersDal
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Folder? Parent { get; set; }
        public ICollection<Folder>? Subfolders { get; set; } = new List<Folder>();
        public bool IsRoot { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApplication.Models
{
    public class Category
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
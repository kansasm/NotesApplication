using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApplication.Models
{

        public class Note
        {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        //public DateTime CreatedOn { get; set; }

        public int CategoryId { get; set; }

        public int userId { get; set; }

        public bool IsDeleted { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }
        //  public object User { get; internal set; }
    }
    }

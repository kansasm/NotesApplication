using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Repo;
using NotesApplication.Models;

namespace NotesApplication.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly NotesApplication.Repo.NoteContext _context;

        public IndexModel(NotesApplication.Repo.NoteContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Repo;
using NotesApplication.Models;

namespace NotesApplication.Pages.EnterNotes
{
    public class DetailsModel : PageModel
    {
        private readonly NotesApplication.Repo.NoteContext _context;

        public DetailsModel(NotesApplication.Repo.NoteContext context)
        {
            _context = context;
        }

        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Note = await _context.Notes.SingleOrDefaultAsync(m => m.ID == id);

            if (Note == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

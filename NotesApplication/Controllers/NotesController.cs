using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApplication.Models;
using NotesApplication.Repo;

namespace NotesApplication.Controllers
{
    
    [Produces("application/json")]
    [Route("api/v1/notes")]
    public class NotesController : Controller
    {
        private readonly NoteContext _context;

        public NotesController(NoteContext context)
        {
            _context = context;
        }

        // GET: api/v1/notes
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            var list = _context.Notes
                       .Include(n => n.User)
                       .Include(n => n.Category).ToList();
            return list;
        }

        // GET: api/v1/notes/5
        // Need to add the Include here.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes
                                .Include(n => n.User)
                                .Include(n => n.Category)
                                .SingleOrDefaultAsync(m => m.ID == id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        // PUT: api/v1/notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes([FromRoute] int id, [FromBody] Note notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notes.ID)
            {
                return BadRequest();
            }
            var user = _context.Users.Find(notes.User.ID);
            var category = _context.Categories.Find(notes.Category.ID);

            notes.User = user;
            notes.Category = category;

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/notes
        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] Note notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.Find(notes.User.ID);
            var category = _context.Categories.Find(notes.Category.ID);

            var noteToCreate = new Note
            {
                User = user,
                Category = category,
                Notes = notes.Notes,
                Title = notes.Title,
               // CreatedOn = notes.CreatedOn,
                IsDeleted = notes.IsDeleted
            };

            _context.Notes.Add(noteToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotes", new { id = notes.ID }, notes);
        }

        // DELETE: api/v1/notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.SingleOrDefaultAsync(m => m.ID == id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return Ok(notes);
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.ID == id);
        }
    }
}
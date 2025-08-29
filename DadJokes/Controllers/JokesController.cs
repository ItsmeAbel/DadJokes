using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadJokes.Data;
using DadJokes.Models;
using Microsoft.AspNetCore.Authorization;

namespace DadJokes.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int _upvote;
        public int _downvote;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Jokes
        public async Task<IActionResult> Index()
        {
              return _context.Joke != null ? 
                          View(await _context.Joke.ToListAsync()) : //returns list
                          Problem("Entity set 'ApplicationDbContext.Joke'  is null.");
        }

        // GET: Jokes/SearchJokes
        public async Task<IActionResult> SearchJokes()
        {
            return View(); //could put in name of view in paranthesis, alternatively if empty, it takes the name of the method auto
        }

        // PoST: Jokes/SearchJokesResults
        public async Task<IActionResult> SearchJokesResults(String SearchPhrase)
        {
            return View("Index", await _context.Joke.Where(J => J.Question.Contains(SearchPhrase)).ToListAsync()); //could put in name of view in paranthesis, alternatively if empty, it takes the name of the method auto
        }

        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Joke == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // GET: Jokes/Create
        [Authorize] //Requires Login
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Joke joke)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joke);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Joke == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Joke joke)
        {
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joke);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeExists(joke.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Joke == null)
            {
                return NotFound();
            }

            var joke = await _context.Joke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: Jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Joke == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Joke'  is null.");
            }
            var joke = await _context.Joke.FindAsync(id);
            if (joke != null)
            {
                _context.Joke.Remove(joke);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //handles riddle upvotes
        [Authorize]
        public async Task<IActionResult> Upvote(int id)
        {

            var joke = await _context.Joke.FindAsync(id);
            if (joke == null) return NotFound();

            joke.Upvote += 1;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        //handles riddle downvotes
        [Authorize]
        public async Task<IActionResult> Downvote(int id)
        {

            var joke = await _context.Joke.FindAsync(id);
            if(joke == null) return NotFound();

            joke.Downvote += 1;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        private bool JokeExists(int id)
        {
          return (_context.Joke?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

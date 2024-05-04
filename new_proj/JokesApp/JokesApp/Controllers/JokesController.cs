using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokesApp.Data;
using JokesApp.Models;

using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JokesApp.Models;

using Microsoft.Extensions.Configuration;

/*namespace JokesApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jokes
        public async Task<IActionResult> Index()
        {
              return _context.joke != null ? 
                          View(await _context.joke.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.joke'  is null.");
        }

        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // GET: Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] joke joke)
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] joke joke)
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
                    if (!jokeExists(joke.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.joke == null)
            {
                return NotFound();
            }

            var joke = await _context.joke
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
            if (_context.joke == null)
            {
                return Problem("Entity set 'ApplicationDbContext.joke'  is null.");
            }
            var joke = await _context.joke.FindAsync(id);
            if (joke != null)
            {
                _context.joke.Remove(joke);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jokeExists(int id)
        {
          return (_context.joke?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}  */

namespace JokesApp.Controllers {

    public class JokesController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public JokesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }


        //Get Jokes Query
        public async Task<IActionResult> Index() {
            using (IDbConnection connection = new SqlConnection(_connectionString)) {

                var jokes = await connection.QueryAsync<joke>("SELECT * FROM joke");
                return View(jokes);

            }
        }

        //Grt Joke details query

        public async Task<IActionResult>Details(int ? id) {

            if(id == null) {
                return NotFound();
            }

            using (IDbConnection connection = new SqlConnection(_connectionString)) {

                var joke = await connection.QueryFirstOrDefaultAsync<joke>("SELECT * FROM JOKE WHERE id = @id", new { Id = id });

                    if(joke == null) {

                    return NotFound();
                }
                return View(joke);

            }
        }

        // GET: Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(joke joke)
        {
            if (ModelState.IsValid)
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO joke (JokeQuestion, JokeAnswer) VALUES (@JokeQuestion, @JokeAnswer)";
                    await connection.ExecuteAsync(query, joke);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var joke = await connection.QueryFirstOrDefaultAsync<joke>("SELECT * FROM joke WHERE Id = @Id", new { Id = id });
                if (joke == null)
                {
                    return NotFound();
                }
                return View(joke);
            }
        }

        // POST: Jokes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, joke joke)
        {
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE joke SET JokeQuestion = @JokeQuestion, JokeAnswer = @JokeAnswer WHERE Id = @Id";
                    await connection.ExecuteAsync(query, joke);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var joke = await connection.QueryFirstOrDefaultAsync<joke>("SELECT * FROM joke WHERE Id = @Id", new { Id = id });
                if (joke == null)
                {
                    return NotFound();
                }

                return View(joke);
            }
        }

        // POST: Jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM joke WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FriendsListApp.Data;
using FriendsListApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace FriendsListApp.Controllers
{
    [Authorize]
    public class RelationsController : Controller
    {
        private readonly FriendsListContext _context;

        public RelationsController(FriendsListContext context)
        {
            _context = context;    
        }

        // GET: Relations
        public async Task<IActionResult> Index()
        {
            var friendsListContext = _context.Relations.Include(r => r.Friend);
            return View(await friendsListContext.ToListAsync());
        }

        // GET: Relations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relations
                .Include(r => r.Friend)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // GET: Relations/Create
        public IActionResult Create()
        {
            ViewData["FriendID"] = new SelectList(_context.Friends, "ID", "ID");
            return View();
        }

        // POST: Relations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,FriendID,Description")] Relation relation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(relation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewData["FriendID"] = new SelectList(_context.Friends, "ID", "ID", relation.FriendID);
            }
           catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(relation);
        }

        // GET: Relations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relations.SingleOrDefaultAsync(m => m.ID == id);
            if (relation == null)
            {
                return NotFound();
            }
            ViewData["FriendID"] = new SelectList(_context.Friends, "ID", "ID", relation.FriendID);
            return View(relation);
        }

        // POST: Relations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id ==null)
            {
                return NotFound();
            }
            var relationToUpdate = await _context.Relations.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Relation>(
                relationToUpdate,
                "",
                s => s.Date, s => s.Description, s => s.FriendID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction("Index");
            }
            ViewData["FriendID"] = new SelectList(_context.Friends, "ID", "ID", relationToUpdate.FriendID);
            return View(relationToUpdate);
        }

        // GET: Relations/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relations
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (relation == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] ="Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            return View(relation);
        }

        // POST: Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relation = await _context.Relations.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if (relation == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Relations.Remove(relation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool RelationExists(int id)
        {
            return _context.Relations.Any(e => e.ID == id);
        }
    }
}

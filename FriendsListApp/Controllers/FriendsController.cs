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
    public class FriendsController : Controller
    {
        private readonly FriendsListContext _context;

        public FriendsController(FriendsListContext context)
        {
            _context = context;    
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            return View(await _context.Friends.ToListAsync());
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends
        .Include(s => s.Relations)
        .AsNoTracking()
        .SingleOrDefaultAsync(m => m.ID == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,NickName,BirthDate,FriendType")] Friend friend)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(friend);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends.SingleOrDefaultAsync(m => m.ID == id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var friendToUpdate = await _context.Friends.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Friend>(
                friendToUpdate,
                "",
                s => s.BirthDate, s => s.FirstMidName, s => s.FriendType, s => s.LastName, s => s.NickName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(friendToUpdate);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friend = await _context.Friends
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (friend==null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Friend friendToDelete = new Friend() { ID = id };
                _context.Entry(friendToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.ID == id);
        }
    }
}

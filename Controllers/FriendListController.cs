using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FriendList.Models;

namespace FriendList.Controllers
{

    [Authorize(Roles = "user")]
    public class FriendListController : Controller
    {
        private readonly FriendListContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendListController(FriendListContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? getOwnerId() {
            return _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        }

        // GET: FriendList
        public async Task<IActionResult> Index()
        {
            var ownerId = getOwnerId();
            return _context.Friend != null ?
                        View(await _context.Friend.Where(frnd => frnd.OwnerId == ownerId).ToListAsync()) :
                        Problem("Entity set 'FriendListContext.Friend'  is null.");
        }

        // GET: FriendList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: FriendList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FriendList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendId,FriendName,Place")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                friend.OwnerId = getOwnerId();
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }

        // GET: FriendList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }else if (friend.OwnerId != getOwnerId()) {
                return new RedirectResult("~/Identity/Account/AccessDenied");
            }

            return View(friend);
        }

        // POST: FriendList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendId,FriendName,Place,OwnerId")] Friend friend)
        {
            if (id != friend.FriendId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(_context?.Friend?.Find(friend.FriendId)?.OwnerId != getOwnerId()) {
                        return new RedirectResult("~/Identity/Account/AccessDenied");
                    }
                    _context?.Update(friend);
                    await _context!.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.FriendId))
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
            return View(friend);
        }

        // GET: FriendList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Friend == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.FriendId == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: FriendList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Friend == null)
            {
                return Problem("Entity set 'FriendListContext.Friend'  is null.");
            }
            var friend = await _context.Friend.FindAsync(id);
            if (friend != null)
            {
                _context.Friend.Remove(friend);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(int id)
        {
            return (_context.Friend?.Any(e => e.FriendId == id)).GetValueOrDefault();
        }
    }
}

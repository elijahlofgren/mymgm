using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mymgm.Data;
using mymgm.Models;

namespace mymgm.Controllers
{
    public class LocalEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalEventsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: LocalEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocalEvent.ToListAsync());
        }

        // GET: LocalEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);
            if (localEvent == null)
            {
                return NotFound();
            }

            return View(localEvent);
        }

        // GET: LocalEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Address,Category,Description,StartDate,Title,Url")] LocalEvent localEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(localEvent);
        }

        // GET: LocalEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);
            if (localEvent == null)
            {
                return NotFound();
            }
            return View(localEvent);
        }

        // POST: LocalEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Address,Category,Description,StartDate,Title,Url")] LocalEvent localEvent)
        {
            if (id != localEvent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalEventExists(localEvent.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(localEvent);
        }

        // GET: LocalEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);
            if (localEvent == null)
            {
                return NotFound();
            }

            return View(localEvent);
        }

        // POST: LocalEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);
            _context.LocalEvent.Remove(localEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LocalEventExists(int id)
        {
            return _context.LocalEvent.Any(e => e.ID == id);
        }
    }
}

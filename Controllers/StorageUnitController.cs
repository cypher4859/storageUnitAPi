using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using storageUnitAPi.Data;
using storageUnitAPi.Models;

namespace storageUnitAPi.Controllers
{
    public class StorageUnitController : Controller
    {
        private readonly DataContext _context;

        public StorageUnitController(DataContext context)
        {
            _context = context;
        }

        // GET: StorageUnit
        public async Task<IActionResult> Index()
        {
            var storageContext = _context.StorageUnit.Include(s => s.CurrentOwner);
            return View(await storageContext.ToListAsync());
        }

        // GET: StorageUnit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageUnit = await _context.StorageUnit
                .Include(s => s.CurrentOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageUnit == null)
            {
                return NotFound();
            }

            return View(storageUnit);
        }

        // GET: StorageUnit/Create
        public IActionResult Create()
        {
            ViewData["CurrentOwnerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id");
            return View();
        }

        // POST: StorageUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservationStartDate,CurrentOwnerId,PreviousOwnersIds,Size,Status")] StorageUnit storageUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storageUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrentOwnerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id", storageUnit.CurrentOwnerId);
            return View(storageUnit);
        }

        // GET: StorageUnit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageUnit = await _context.StorageUnit.FindAsync(id);
            if (storageUnit == null)
            {
                return NotFound();
            }
            ViewData["CurrentOwnerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id", storageUnit.CurrentOwnerId);
            return View(storageUnit);
        }

        // POST: StorageUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservationStartDate,CurrentOwnerId,PreviousOwnersIds,Size,Status")] StorageUnit storageUnit)
        {
            if (id != storageUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storageUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageUnitExists(storageUnit.Id))
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
            ViewData["CurrentOwnerId"] = new SelectList(_context.Set<Customer>(), "Id", "Id", storageUnit.CurrentOwnerId);
            return View(storageUnit);
        }

        // GET: StorageUnit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageUnit = await _context.StorageUnit
                .Include(s => s.CurrentOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageUnit == null)
            {
                return NotFound();
            }

            return View(storageUnit);
        }

        // POST: StorageUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storageUnit = await _context.StorageUnit.FindAsync(id);
            if (storageUnit != null)
            {
                _context.StorageUnit.Remove(storageUnit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageUnitExists(int id)
        {
            return _context.StorageUnit.Any(e => e.Id == id);
        }
    }
}

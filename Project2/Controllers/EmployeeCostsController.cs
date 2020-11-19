using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project2.Controllers
{
    [Authorize]
    public class EmployeeCostsController : Controller
    {
        private readonly Dimentia_DatabaseContext _context;

        public EmployeeCostsController(Dimentia_DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager")]
        // GET: EmployeeCosts
        public async Task<IActionResult> Index()
        {
            var dimentia_DatabaseContext = _context.EmployeeCost.Include(e => e.EmployeeNumberNavigation);
            return View(await dimentia_DatabaseContext.ToListAsync());
        }

        // GET: EmployeeCosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCost = await _context.EmployeeCost
                .Include(e => e.EmployeeNumberNavigation)
                .FirstOrDefaultAsync(m => m.CostId == id);
            if (employeeCost == null)
            {
                return NotFound();
            }

            return View(employeeCost);
        }

        // GET: EmployeeCosts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber");
            return View();
        }

        // POST: EmployeeCosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostId,DailyRate,EmployeeNumber,HourlyRate,MonthlyIncome,MonthlyRate,OverTime,PercentSalaryHike,StandardHours")] EmployeeCost employeeCost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", employeeCost.EmployeeNumber);
            return View(employeeCost);
        }

        // GET: EmployeeCosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCost = await _context.EmployeeCost.FindAsync(id);
            if (employeeCost == null)
            {
                return NotFound();
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", employeeCost.EmployeeNumber);
            return View(employeeCost);
        }

        // POST: EmployeeCosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CostId,DailyRate,EmployeeNumber,HourlyRate,MonthlyIncome,MonthlyRate,OverTime,PercentSalaryHike,StandardHours")] EmployeeCost employeeCost)
        {
            if (id != employeeCost.CostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeCost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeCostExists(employeeCost.CostId))
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
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", employeeCost.EmployeeNumber);
            return View(employeeCost);
        }

        // GET: EmployeeCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCost = await _context.EmployeeCost
                .Include(e => e.EmployeeNumberNavigation)
                .FirstOrDefaultAsync(m => m.CostId == id);
            if (employeeCost == null)
            {
                return NotFound();
            }

            return View(employeeCost);
        }

        // POST: EmployeeCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeCost = await _context.EmployeeCost.FindAsync(id);
            _context.EmployeeCost.Remove(employeeCost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeCostExists(int id)
        {
            return _context.EmployeeCost.Any(e => e.CostId == id);
        }
    }
}

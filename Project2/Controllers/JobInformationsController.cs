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

    public class JobInformationsController : Controller
    {
        private readonly Dimentia_DatabaseContext _context;

        public JobInformationsController(Dimentia_DatabaseContext context)
        {
            _context = context;
        }

        // GET: JobInformations
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            var dimentia_DatabaseContext = _context.JobInformation.Include(j => j.EmployeeNumberNavigation);
            return View(await dimentia_DatabaseContext.ToListAsync());
        }

        // GET: JobInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInformation = await _context.JobInformation
                .Include(j => j.EmployeeNumberNavigation)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobInformation == null)
            {
                return NotFound();
            }

            return View(jobInformation);
        }

        // GET: JobInformations/Create
        public IActionResult Create()
        {
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber");
            return View();
        }

        // POST: JobInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,BusinessTravel,Department,EmployeeNumber,JobInvolvement,JobLevel,JobRole")] JobInformation jobInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", jobInformation.EmployeeNumber);
            return View(jobInformation);
        }

        // GET: JobInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInformation = await _context.JobInformation.FindAsync(id);
            if (jobInformation == null)
            {
                return NotFound();
            }
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", jobInformation.EmployeeNumber);
            return View(jobInformation);
        }

        // POST: JobInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,BusinessTravel,Department,EmployeeNumber,JobInvolvement,JobLevel,JobRole")] JobInformation jobInformation)
        {
            if (id != jobInformation.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobInformationExists(jobInformation.JobId))
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
            ViewData["EmployeeNumber"] = new SelectList(_context.EmployeeDetails, "EmployeeNumber", "EmployeeNumber", jobInformation.EmployeeNumber);
            return View(jobInformation);
        }

        // GET: JobInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInformation = await _context.JobInformation
                .Include(j => j.EmployeeNumberNavigation)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobInformation == null)
            {
                return NotFound();
            }

            return View(jobInformation);
        }

        // POST: JobInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobInformation = await _context.JobInformation.FindAsync(id);
            _context.JobInformation.Remove(jobInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobInformationExists(int id)
        {
            return _context.JobInformation.Any(e => e.JobId == id);
        }
    }
}

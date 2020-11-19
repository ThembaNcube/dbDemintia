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
    public class EmployeeDetailsController : Controller
    {
        private readonly Dimentia_DatabaseContext _context;

        public EmployeeDetailsController(Dimentia_DatabaseContext context)
        {
            _context = context;
        }

        //[Authorize(Roles ="Admin","Manager")]
        // GET: EmployeeDetails
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeDetails.ToListAsync());
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Index(string getField)
        {
            ViewData["GetField"] = getField;
            var fQuery = from x in _context.EmployeeDetails select x;
            if (!String.IsNullOrEmpty(getField))
            {
                fQuery = fQuery.Where(x => x.Department.Contains(getField));
            }
            return View(await fQuery.AsNoTracking().ToListAsync());
        }

        // GET: EmployeeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            return View(employeeDetails);
        }


        // GET: EmployeeDetails/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,Age,Attrition,Department,DistanceFromHome,Education,EducationField,EmployeeNumber,Gender,MaritalStatus,Over18")] EmployeeDetails employeeDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDetails);
        }



        // GET: EmployeeDetails/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetails == null)
            {
                return NotFound();
            }
            return View(employeeDetails);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,Age,Attrition,Department,DistanceFromHome,Education,EducationField,EmployeeNumber,Gender,MaritalStatus,Over18")] EmployeeDetails employeeDetails)
        {
            if (id != employeeDetails.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDetailsExists(employeeDetails.EmployeeNumber))
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
            return View(employeeDetails);
        }


        // GET: EmployeeDetails/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetails = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            return View(employeeDetails);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDetails = await _context.EmployeeDetails.FindAsync(id);
            _context.EmployeeDetails.Remove(employeeDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDetailsExists(int id)
        {
            return _context.EmployeeDetails.Any(e => e.EmployeeNumber == id);
        }
    }
}

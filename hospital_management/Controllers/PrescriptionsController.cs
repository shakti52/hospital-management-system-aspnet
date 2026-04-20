using hospital_management.Data;
using hospital_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace hospital_management.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly HospitalContext _context;

        public PrescriptionsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: /Prescriptions
        public async Task<IActionResult> Index()
        {
            var prescriptions = _context.Prescriptions
                .Include(p => p.Patient); // Include patient name
            return View(await prescriptions.ToListAsync());
        }

        // GET: /Prescriptions/Create
        public IActionResult Create()
        {
            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        // POST: /Prescriptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Prescriptions.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "Name", prescription.PatientId);
            return View(prescription);
        }

        // GET: /Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null) return NotFound();

            return View(prescription);
        }

        // GET: /Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return NotFound();

            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "Name", prescription.PatientId);
            return View(prescription);
        }

        // POST: /Prescriptions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prescription prescription)
        {
            if (id != prescription.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Prescriptions.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "Name", prescription.PatientId);
            return View(prescription);
        }

        // POST: /Prescriptions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

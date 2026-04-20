using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hospital_management.Data;
using hospital_management.Models;

namespace hospital_management.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalContext _context;

        public AppointmentsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor);
            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,DoctorId,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Prevent double booking
                var isDoctorBusy = await _context.Appointments
                    .AnyAsync(a => a.DoctorId == appointment.DoctorId && a.AppointmentDate == appointment.AppointmentDate);

                if (isDoctorBusy)
                {
                    ModelState.AddModelError("", "Doctor is already booked at this time.");
                    ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
                    ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", appointment.DoctorId);
                    return View(appointment);
                }

                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", appointment.DoctorId);

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DoctorId,AppointmentDate")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name", appointment.DoctorId);

            return View(appointment);
        }

    }
}

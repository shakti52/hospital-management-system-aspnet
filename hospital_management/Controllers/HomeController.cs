using Microsoft.AspNetCore.Mvc;
using hospital_management.Data;
using System;
using System.Linq;

namespace hospital_management.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalContext _context;

        public HomeController(HospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
            ViewBag.TotalPatients = _context.Patients?.Count() ?? 0;
            ViewBag.TotalDoctors = _context.Doctors?.Count() ?? 0;
            ViewBag.TotalAppointments = _context.Appointments?
                .Where(a => a.AppointmentDate.Date == DateTime.Today)
                .Count() ?? 0;
            ViewBag.TotalPrescriptions = _context.Prescriptions?.Count() ?? 0;

            return View();
        }
    }
}

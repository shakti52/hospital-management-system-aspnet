using hospital_management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

namespace hospital_management.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
    }
}

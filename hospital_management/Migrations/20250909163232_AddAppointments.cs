using System;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.Models
{
    public class Appointments
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctors Doctor { get; set; }
    }
}

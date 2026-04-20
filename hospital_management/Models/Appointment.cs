using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace hospital_management.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        // Navigation properties (links to related tables)
        public Patient? Patient { get; set; }
        public Doctors? Doctor { get; set; }
    }
}

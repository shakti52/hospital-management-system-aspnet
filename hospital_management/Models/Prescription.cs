using System;
using System.ComponentModel.DataAnnotations;

namespace hospital_management.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }       // FK to Patient
        public Patient Patient { get; set; }

        [Required]
        public string Medication { get; set; }

        public string Notes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace hospital_management.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

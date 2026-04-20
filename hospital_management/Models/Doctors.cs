using System.ComponentModel.DataAnnotations;

namespace hospital_management.Models
{
    public class Doctors
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Specialization { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

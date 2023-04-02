using System.ComponentModel.DataAnnotations;

namespace ClinicaDentariaProjeto.Models
{
    public class Team
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = "";

        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "NIF")]
        public int NIF { get; set; }

        [Display(Name = "Position")]
        public string? Position { get; set; }

        [Display(Name = "Expertise")]
        public string? Expertise { get; set; }

    }
}

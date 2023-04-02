using Microsoft.AspNetCore.Mvc.Localization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ClinicaDentariaProjeto.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = "";
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; } = DateTime.Now;
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "NIF")]
        public int NIF { get; set; }
        [Display(Name = "Health Care")]
        public int HealthCare { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaDentariaProjeto.Models
{
    public class Factura
    {
        public int ID { get; set; }

        [Display(Name = "Number")]
        public int NumberFactura { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price")]
        public float PriceFactura { get; set; }
        [Display(Name = "Payment Confirme")]
        public bool PaymentConfirme { get; set; }

        //Foreign Key
        [Display(Name = "Appointment Reference")]
        public int ConsultaID { get; set; }
        public Consulta? Consulta { get; set; }
    }
}

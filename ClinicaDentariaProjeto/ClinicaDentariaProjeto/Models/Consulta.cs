﻿using System.ComponentModel.DataAnnotations;

namespace ClinicaDentariaProjeto.Models
{
    public class Consulta
    {
        public int ID { get; set; }
        [Display(Name = "Number of Appointment")]
        public int NumberConsulta { get; set; }
        [Display(Name = "Day and Time")]
        public DateTime DayTime { get; set; }
        public bool Affirmative { get; set; }
        public string? Observation { get; set; }

        //Foreign Key
        public int ClientID { get; set; }
        public Client? Client { get; set; }
        public int TeamID { get; set; }
        public Team? Team { get; set; }
    }
}

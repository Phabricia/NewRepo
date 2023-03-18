﻿namespace ClinicaDentariaProjeto.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public int NIF { get; set; }
        public int HealthCare { get; set; }
    }
}
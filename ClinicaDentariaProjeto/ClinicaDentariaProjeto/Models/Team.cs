namespace ClinicaDentariaProjeto.Models
{
    public class Team
    {
        public int ID { get; set; }

        public string Name { get; set; } = "";

        public DateTime Birthday { get; set; }

        public string? Address { get; set; }

        public int NIF { get; set; }

        public string? Position { get; set; }

        public string? Expertise { get; set; }

    }
}

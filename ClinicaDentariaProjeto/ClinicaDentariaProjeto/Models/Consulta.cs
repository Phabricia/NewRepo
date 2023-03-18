namespace ClinicaDentariaProjeto.Models
{
    public class Consulta
    {
        public int ID { get; set; }
        public int NumberConsulta { get; set; }
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

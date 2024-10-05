namespace HealthMed.Domain.Entities
{
    public class HorarioDisponivel
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; } = true;
        public Medico Medico { get; set; }
    }
}

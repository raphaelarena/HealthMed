namespace HealthMed.Domain.Entities
{
    public class HorarioDisponivel
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataDisponibilidade { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public Medico Medico { get; set; }
    }
}

namespace HealthMed.Application.DTOs
{
    public class AgendamentoDto
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
    }
}

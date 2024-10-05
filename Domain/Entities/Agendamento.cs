using System;
using System.ComponentModel.DataAnnotations;

namespace HealthMed.Domain.Entities
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthMed.Domain.Entities
{
    public class AgendamentoAudit
    {
        public int Id { get; set; }
        public int AgendamentoId { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Alteracao { get; set; }

        public Agendamento Agendamento { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace HealthMed.Domain.Entities
{
    public class HorarioDisponivelAudit
    {
        public int Id { get; set; }
        public int HorarioDisponivelId { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Alteracao { get; set; }

        public HorarioDisponivel HorarioDisponivel { get; set; }
    }
}
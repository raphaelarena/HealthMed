using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infrastructure.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly HealthMedDbContext _context;

        public AgendamentoRepository(HealthMedDbContext context)
        {
            _context = context;
        }

        public async Task Add(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAgendamentoParaHorario(int medicoId, DateTime dataHora)
        {
            return await _context.Agendamentos.AnyAsync(a => a.MedicoId == medicoId && a.DataHora == dataHora);
        }

        public async Task AtualizarAgendamento(Agendamento agendamento, string usuario)
        {
            var agendamentoExistente = await _context.Agendamentos.FindAsync(agendamento.Id);
            if (agendamentoExistente == null) throw new KeyNotFoundException("Agendamento não encontrado.");

            agendamentoExistente.DataHora = agendamento.DataHora;

            var audit = new AgendamentoAudit
            {
                AgendamentoId = agendamento.Id,
                ModificadoPor = usuario,
                DataModificacao = DateTime.Now,
                Alteracao = $"Data e Hora alterados para {agendamento.DataHora}"
            };

            _context.AgendamentoAudits.Add(audit);
            _context.Agendamentos.Update(agendamentoExistente);
            await _context.SaveChangesAsync();
        }
    }

}

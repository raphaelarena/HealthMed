using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Interface
{
    public interface IAgendamentoRepository
    {
        Task Add(Agendamento agendamento);
        Task<bool> ExisteAgendamentoParaHorario(int medicoId, DateTime dataHora);
        Task AtualizarAgendamento(Agendamento agendamento, string usuario);
    }

}

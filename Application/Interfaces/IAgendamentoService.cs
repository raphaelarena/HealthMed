using HealthMed.Application.DTOs;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IAgendamentoService
    {
        Task AgendarConsulta(AgendamentoDto dto);
        Task AtualizarAgendamento(Agendamento agendamento, string usuario);
    }
}

using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IEmailService
    {
        Task EnviarNotificacaoConsulta(Agendamento agendamento);
    }
}

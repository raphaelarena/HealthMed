using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public async Task EnviarNotificacaoConsulta(Agendamento agendamento)
        {
            var medicoEmail = agendamento.Medico.Email;
            var message = $"Olá, Dr. {agendamento.Medico.Nome}!\n" +
                          $"Você tem uma nova consulta marcada! Paciente: {agendamento.Paciente.Nome}.\n" +
                          $"Data e horário: {agendamento.DataHora}.\n";
        }
    }
}

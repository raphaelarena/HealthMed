using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using System.Net.Mail;
using System.Net;
using HealthMed.Configurations;

namespace HealthMed.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task EnviarNotificacaoConsulta(Agendamento agendamento)
        {
            var medicoEmail = agendamento.Medico.Email;
            var body = $"Olá, Dr. {agendamento.Medico.Nome}!\n" +
                          $"Você tem uma nova consulta marcada! Paciente: {agendamento.Paciente.Nome}.\n" +
                          $"Data e horário: {agendamento.DataHora}.\n";

            var fromAddress = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName);
            var toAddress = new MailAddress(medicoEmail);

            var smtp = new SmtpClient
            {
                Host = _emailSettings.SmtpServer,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _emailSettings.Password)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Nova Consulta", // Defina um assunto apropriado
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}

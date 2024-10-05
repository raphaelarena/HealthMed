using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Exceptions;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Interface;

namespace HealthMed.Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IEmailService _emailService;
        private readonly HealthMedDbContext _context;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IEmailService emailService, HealthMedDbContext context)
        {
            _agendamentoRepository = agendamentoRepository;
            _emailService = emailService;
            _context = context;
        }

        public async Task AgendarConsulta(AgendamentoDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var conflitos = await _agendamentoRepository.ExisteAgendamentoParaHorario(dto.MedicoId, dto.DataHora);
                if (conflitos)
                {
                    throw new AgendamentoConflitoException("Já existe uma consulta agendada para este horário.");
                }

                var agendamento = new Agendamento
                {
                    MedicoId = dto.MedicoId,
                    PacienteId = dto.PacienteId,
                    DataHora = dto.DataHora
                };

                await _agendamentoRepository.Add(agendamento);
                await _emailService.EnviarNotificacaoConsulta(agendamento);

                await transaction.CommitAsync();
            }
        }

        public async Task AtualizarAgendamento(Agendamento agendamento, string usuario)
        {
            await _agendamentoRepository.AtualizarAgendamento(agendamento, usuario);
        }
    }

}

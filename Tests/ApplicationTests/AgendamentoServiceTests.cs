using Moq;
using Xunit;
using HealthMed.Infrastructure.Interface;
using HealthMed.Application.Interfaces;
using HealthMed.Infrastructure.Data;
using HealthMed.Application.Services;
using HealthMed.Application.DTOs;
using Microsoft.EntityFrameworkCore.Storage;
using HealthMed.Domain.Entities;


namespace HealthMed.Tests.ApplicationTests
{
    public class AgendamentoServiceTests
    {
        private readonly Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<HealthMedDbContext> _dbContextMock;
        private readonly AgendamentoService _agendamentoService;

        public AgendamentoServiceTests()
        {
            _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _dbContextMock = new Mock<HealthMedDbContext>();
            _agendamentoService = new AgendamentoService(_agendamentoRepositoryMock.Object, _emailServiceMock.Object, _dbContextMock.Object);
        }

        [Fact]
        public async Task AgendarConsulta_DeveEnviarEmailECommitarTransacao()
        {
            var agendamentoDto = new AgendamentoDto
            {
                MedicoId = 1,
                PacienteId = 1,
                DataHora = DateTime.Now.AddHours(1)
            };

            var dbTransactionMock = new Mock<IDbContextTransaction>();
            _dbContextMock.Setup(d => d.Database.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(dbTransactionMock.Object);

            await _agendamentoService.AgendarConsulta(agendamentoDto);

            _agendamentoRepositoryMock.Verify(repo => repo.Add(It.IsAny<Agendamento>()), Times.Once);
            _emailServiceMock.Verify(service => service.EnviarNotificacaoConsulta(It.IsAny<Agendamento>()), Times.Once);
            dbTransactionMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

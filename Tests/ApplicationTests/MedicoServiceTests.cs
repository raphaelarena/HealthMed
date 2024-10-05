using Moq;
using Xunit;
using HealthMed.Application.DTOs;
using HealthMed.Application.Services;
using HealthMed.Infrastructure.Interface;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;

namespace HealthMed.Tests.ApplicationTests
{
    public class MedicoServiceTests
    {
        private readonly Mock<IMedicoRepository> _medicoRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly MedicoService _medicoService;

        public MedicoServiceTests()
        {
            _medicoRepositoryMock = new Mock<IMedicoRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _medicoService = new MedicoService(_medicoRepositoryMock.Object, _passwordHasherMock.Object);
        }

        [Fact]
        public async Task CadastrarMedico_DeveSalvarMedicoComSenhaHasheada()
        {
            var medicoDto = new MedicoDto
            {
                Nome = "Dr. João",
                CPF = "12345678901",
                CRM = "CRM12345",
                Email = "joao@medico.com",
                Senha = "senha123"
            };

            _passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<string>())).Returns("hashedPassword");

            await _medicoService.CadastrarMedico(medicoDto);

            _medicoRepositoryMock.Verify(repo => repo.Add(It.Is<Medico>(m => m.Senha == "hashedPassword")), Moq.Times.Once);
        }
    }
}

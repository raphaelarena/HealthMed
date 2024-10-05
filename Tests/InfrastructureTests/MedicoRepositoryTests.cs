using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Repositories;

namespace HealthMed.Tests.InfrastructureTests
{
    public class MedicoRepositoryTests
    {
        private readonly HealthMedDbContext _dbContext;
        private readonly MedicoRepository _medicoRepository;

        public MedicoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<HealthMedDbContext>()
                .UseInMemoryDatabase(databaseName: "HealthMedTest")
                .Options;

            _dbContext = new HealthMedDbContext(options);
            _medicoRepository = new MedicoRepository(_dbContext);
        }

        [Fact]
        public async Task Add_DeveSalvarMedicoNoBanco()
        {
            var medico = new Medico
            {
                Nome = "Dr. Pedro",
                CPF = "12345678902",
                CRM = "CRM54321",
                Email = "pedro@medico.com",
                Senha = "hashedPassword"
            };

            await _medicoRepository.Add(medico);
            var medicoSalvo = await _dbContext.Medicos.FindAsync(medico.Id);

            Assert.NotNull(medicoSalvo);
            Assert.Equal("Dr. Pedro", medicoSalvo.Nome);
        }

        [Fact]
        public async Task GetByEmail_DeveRetornarMedicoPeloEmail()
        {
            var medico = new Medico
            {
                Nome = "Dr. Ana",
                CPF = "12345678903",
                CRM = "CRM67890",
                Email = "ana@medico.com",
                Senha = "hashedPassword"
            };

            await _dbContext.Medicos.AddAsync(medico);
            await _dbContext.SaveChangesAsync();

            var medicoRetornado = await _medicoRepository.GetByEmail("ana@medico.com");

            Assert.NotNull(medicoRetornado);
            Assert.Equal("Dr. Ana", medicoRetornado.Nome);
        }
    }
}

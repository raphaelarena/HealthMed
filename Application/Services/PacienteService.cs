using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Interface;

namespace HealthMed.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPasswordHasher _passwordHasher;

        public PacienteService(IPacienteRepository pacienteRepository, IPasswordHasher passwordHasher)
        {
            _pacienteRepository = pacienteRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task CadastrarPaciente(PacienteDto pacienteDto)
        {
            var paciente = new Paciente
            {
                Nome = pacienteDto.Nome,
                CPF = pacienteDto.CPF,
                Email = pacienteDto.Email,
                Senha = _passwordHasher.HashPassword(pacienteDto.Senha)
            };

            await _pacienteRepository.Add(paciente);
        }
    }

}

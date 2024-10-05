using HealthMed.Application.Interfaces;
using HealthMed.Infrastructure.Interface;

namespace HealthMed.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
        {
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _jwtTokenService = jwtTokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> LoginMedicoAsync(string email, string senha)
        {
            var medico = await _medicoRepository.GetByEmail(email);
            if (medico == null || !_passwordHasher.VerifyHashedPassword(medico.Senha, senha))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            return _jwtTokenService.GenerateToken(medico.Email, "Medico");
        }

        public async Task<string> LoginPacienteAsync(string email, string senha)
        {
            var paciente = await _pacienteRepository.GetByEmail(email);
            if (paciente == null || !_passwordHasher.VerifyHashedPassword(paciente.Senha, senha))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            return _jwtTokenService.GenerateToken(paciente.Email, "Paciente");
        }
    }

}

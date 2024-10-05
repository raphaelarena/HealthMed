using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login/medico")]
        public async Task<IActionResult> LoginMedico([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginMedicoAsync(loginDto.Email, loginDto.Senha);
            return Ok(new { Token = token });
        }

        [HttpPost("login/paciente")]
        public async Task<IActionResult> LoginPaciente([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginPacienteAsync(loginDto.Email, loginDto.Senha);
            return Ok(new { Token = token });
        }
    }

}

using HealthMed.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using HealthMed.Application.Interfaces;

namespace HealthMed.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarPaciente([FromBody] PacienteDto pacienteDto)
        {
            await _pacienteService.CadastrarPaciente(pacienteDto);
            return Ok("Paciente cadastrado com sucesso.");
        }
    }

}

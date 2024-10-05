using HealthMed.Application.DTOs;
using HealthMed.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarMedico([FromBody] MedicoDto medicoDto)
        {
            await _medicoService.CadastrarMedico(medicoDto);
            return Ok("Médico cadastrado com sucesso.");
        }
    }
}

using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Services;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarMedico([FromBody] MedicoDto medicoDto)
        {
            await _medicoService.CadastrarMedico(medicoDto);
            return Ok("Médico cadastrado com sucesso.");
        }

        [HttpPost("cadastrar-horario-disponivel")]
        [Authorize]
        public async Task<IActionResult> CadastrarHorarioDisponivel([FromBody] HorarioDisponivel horario)
        {
            await _medicoService.CadastrarHorarioDisponivel(horario);
            return CreatedAtAction(nameof(ObterHorariosDisponiveis), new { medicoId = horario.MedicoId }, horario);
        }

        [HttpPut("editar-horario-disponivel")]
        [Authorize]
        public async Task<IActionResult> EditarHorarioDisponivel([FromBody] HorarioDisponivel horario)
        {
            await _medicoService.EditarHorarioDisponivel(horario);
            return NoContent();
        }

        [HttpGet("{medicoId}")]
        [Authorize]
        public async Task<ActionResult<List<HorarioDisponivel>>> ObterHorariosDisponiveis(int medicoId)
        {
            var horarios = await _medicoService.ObterHorariosDisponiveis(medicoId);
            return Ok(horarios);
        }
    }
}

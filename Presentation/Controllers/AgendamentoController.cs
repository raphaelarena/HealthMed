using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpPost("agendar")]
        [Authorize]
        public async Task<IActionResult> AgendarConsulta([FromBody] AgendamentoDto agendamentoDto)
        {
            await _agendamentoService.AgendarConsulta(agendamentoDto);
            return Ok("Consulta agendada com sucesso.");
        }

        [HttpPut("atualizar/{id}")]
        [Authorize]
        public async Task<IActionResult> AtualizarAgendamento(int id, [FromBody] AgendamentoDto agendamentoDto)
        {
            var agendamento = new Agendamento
            {
                Id = id,
                MedicoId = agendamentoDto.MedicoId,
                PacienteId = agendamentoDto.PacienteId,
                DataHora = agendamentoDto.DataHora
            };

            var usuario = User.Identity.Name;

            await _agendamentoService.AtualizarAgendamento(agendamento, usuario);
            return Ok("Consulta atualizada com sucesso.");
        }
    }

}

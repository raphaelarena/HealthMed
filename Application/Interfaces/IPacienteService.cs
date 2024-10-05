using HealthMed.Application.DTOs;

namespace HealthMed.Application.Interfaces
{
    public interface IPacienteService
    {
        Task CadastrarPaciente(PacienteDto pacienteDto);
    }

}

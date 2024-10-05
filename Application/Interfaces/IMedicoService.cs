using HealthMed.Application.DTOs;

namespace HealthMed.Application.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarMedico(MedicoDto medicoDto);
    }

}

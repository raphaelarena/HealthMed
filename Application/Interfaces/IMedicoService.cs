using HealthMed.Application.DTOs;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarMedico(MedicoDto medicoDto);
        Task CadastrarHorarioDisponivel(HorarioDisponivel horario);
        Task EditarHorarioDisponivel(HorarioDisponivel horario);
        Task<List<HorarioDisponivel>> ObterHorariosDisponiveis(int medicoId);
    }

}

using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Interface
{
    public interface IMedicoRepository
    {
        Task Add(Medico medico);
        Task<Medico> GetByEmail(string email);
        Task AdicionarHorarioDisponivel(HorarioDisponivel horario);
        Task AtualizarHorarioDisponivel(HorarioDisponivel horario);
        Task<List<HorarioDisponivel>> ObterHorariosDisponiveisPorMedicoId(int medicoId);
        Task<HorarioDisponivel> ObterHorarioDisponivelPorId(int id);
    }
}

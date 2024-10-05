using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Interface
{
    public interface IMedicoRepository
    {
        Task Add(Medico medico);
        Task<Medico> GetByEmail(string email);
    }
}

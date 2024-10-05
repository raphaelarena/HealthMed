using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Interface
{
    public interface IPacienteRepository
    {
        Task Add(Paciente paciente);
        Task<Paciente> GetByEmail(string email);
    }

}

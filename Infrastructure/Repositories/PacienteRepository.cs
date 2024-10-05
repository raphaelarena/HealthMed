using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HealthMedDbContext _context;

        public PacienteRepository(HealthMedDbContext context)
        {
            _context = context;
        }

        public async Task Add(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<Paciente> GetByEmail(string email)
        {
            return await _context.Pacientes.SingleOrDefaultAsync(p => p.Email == email);
        }
    }

}

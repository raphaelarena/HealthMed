using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly HealthMedDbContext _context;

        public MedicoRepository(HealthMedDbContext context)
        {
            _context = context;
        }

        public async Task Add(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
        }

        public async Task<Medico> GetByEmail(string email)
        {
            return await _context.Medicos.SingleOrDefaultAsync(m => m.Email == email);
        }
    }

}

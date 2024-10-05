﻿using HealthMed.Domain.Entities;
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

        public async Task AdicionarHorarioDisponivel(HorarioDisponivel horario)
        {
            await _context.HorariosDisponiveis.AddAsync(horario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarHorarioDisponivel(HorarioDisponivel horario)
        {
            _context.HorariosDisponiveis.Update(horario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HorarioDisponivel>> ObterHorariosDisponiveisPorMedicoId(int medicoId)
        {
            return await _context.HorariosDisponiveis
                .Where(h => h.MedicoId == medicoId)
                .ToListAsync();
        }

        public async Task<HorarioDisponivel> ObterHorarioDisponivelPorId(int id)
        {
            return await _context.HorariosDisponiveis.FindAsync(id);
        }
    }

}

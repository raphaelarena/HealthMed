﻿using HealthMed.Application.DTOs;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Interface;
using HealthMed.Infrastructure.Repositories;

namespace HealthMed.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPasswordHasher _passwordHasher;

        public MedicoService(IMedicoRepository medicoRepository, IPasswordHasher passwordHasher)
        {
            _medicoRepository = medicoRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task CadastrarMedico(MedicoDto medicoDto)
        {
            var medico = new Medico
            {
                Nome = medicoDto.Nome,
                CPF = medicoDto.CPF,
                CRM = medicoDto.CRM,
                Email = medicoDto.Email,
                Senha = _passwordHasher.HashPassword(medicoDto.Senha)
            };

            await _medicoRepository.Add(medico);
        }
    }
}

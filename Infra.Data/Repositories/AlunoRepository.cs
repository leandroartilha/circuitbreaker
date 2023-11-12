using System;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private ApplicationDbContext _context;
        public AlunoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> CreateAluno(Aluno aluno)
        {
            _context.Add(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public Task<Aluno> DeleteAluno(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task<Aluno> GetAlunoById(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public Task<Aluno> UpdateAluno(Aluno aluno)
        {
            throw new NotImplementedException();
        }
    }
}


using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoById(int? id);
        Task<Aluno> CreateAluno(Aluno aluno);
        Task<Aluno> DeleteAluno(Aluno aluno);
        Task<Aluno> UpdateAluno(Aluno aluno);
    }
}


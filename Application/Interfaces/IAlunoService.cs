using System;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoDTO>> GetAlunos();
        Task<AlunoDTO> GetAlunoById(int? id);
        Task CreateAluno(AlunoDTO alunoDto);
        Task UpdateAluno(AlunoDTO alunoDto);
        Task DeleteAluno(int id);
    }
}


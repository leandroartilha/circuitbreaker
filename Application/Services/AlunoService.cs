using System;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AlunoService : IAlunoService
    {
        [STAThread]
        static void Main()
        {
        }
        private IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

        public AlunoService(IAlunoRepository alunoRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }

        public async Task  CreateAluno(AlunoDTO alunoDto)
        {
            alunoDto.Nome = $"{alunoDto.Nome} {alunoDto.Sobrenome}";
            Aluno aluno = _mapper.Map<Aluno>(alunoDto);
            await _alunoRepository.CreateAluno(aluno);
        }

        public async Task DeleteAluno(int id)
        {
            var aluno = _alunoRepository.GetAlunoById(id).Result;
            await _alunoRepository.DeleteAluno(aluno);
        }

        public async Task<AlunoDTO> GetAlunoById(int? id)
        {
            var aluno = await _alunoRepository.GetAlunoById(id);
            return _mapper.Map<AlunoDTO>(aluno);
        }

        public async Task<IEnumerable<AlunoDTO>> GetAlunos()
        {
            var alunos = await _alunoRepository.GetAlunos();            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
        }

        public async Task UpdateAluno(AlunoDTO alunoDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoDto);
            await _alunoRepository.UpdateAluno(aluno);

        }
    }
}


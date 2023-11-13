using System;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Polly.CircuitBreaker;
using Application.Services;
using System.Net.Http;

namespace Application.Services
{
    public class AlunoService : IAlunoService
    {
        [STAThread]
        static void Main()
        {
        }
        private IAlunoRepository _alunoRepository;
        private object httpClient;
        private object urlApiContagem;
        private readonly IMapper _mapper;
        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

        public AlunoService(IAlunoRepository alunoRepository, IMapper mapper, AsyncCircuitBreakerPolicy circuitBreaker)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
            _circuitBreaker = circuitBreaker;
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
            var teste = new Rotacao();
            teste.ExecutarCircuitB();

            var resultado = await _circuitBreaker.ExecuteAsync<ResultadoContador>(() =>
            {
                return httpClient
                    .GetFromJsonAsync<ResultadoContador>(urlApiContagem);
            });


            var alunos = await _alunoRepository.GetAlunos();
            return _mapper.Map<IEnumerable<AlunoDTO>>(alunos);


        }

        public async Task UpdateAluno(AlunoDTO alunoDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoDto);
            await _alunoRepository.UpdateAluno(aluno);

        }
    }
}


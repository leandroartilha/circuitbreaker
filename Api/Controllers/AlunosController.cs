using System;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

        public AlunosController(IAlunoService alunoService, AsyncCircuitBreakerPolicy circuitBreaker)
        {
            _alunoService = alunoService;
            _circuitBreaker = circuitBreaker;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        {
            //circuit
            var httpClient = new HttpClient();
            var urlApiContagem = _configuration["UrlApi"];

            var resultado = await _circuitBreaker.ExecuteAsync<Aluno>(() =>
            {
                return httpClient
                    .GetFromJsonAsync<Aluno>(urlApiContagem);
            });

            var alunos = await _alunoService.GetAlunos();

            if(alunos == null)
            {
                return NotFound("Alunos não encontrados");
            }



            return Ok(alunos);
        }

        [HttpPost]
        public async Task<ActionResult> CriarAluno([FromBody] AlunoDTO alunoDTO)
        {
            if(alunoDTO == null)
            {
                return BadRequest();
            }

            await _alunoService.CreateAluno(alunoDTO);

            return Ok($"Aluno {alunoDTO.Nome} adicionado!");
        }
    }
}


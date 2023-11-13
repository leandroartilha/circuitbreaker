using System;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        {


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


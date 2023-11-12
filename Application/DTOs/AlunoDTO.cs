using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Sobrenome { get; set; }

    }
}


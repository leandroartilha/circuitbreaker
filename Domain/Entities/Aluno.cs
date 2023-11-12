using System;
using Domain.Validation;

namespace Domain.Entities
{
    public class Aluno
    {
        [STAThread]
        static void Main()
        {
        }
        public int Id { get; private set; }
        public string Nome  { get; private set; }
        public string? Cpf { get; private set; }
        public DateTime? DataNascimento  { get; private set; }
        public DateTime DataCadastro  { get; private set; }
        public string? Observacoes  { get; private set; }

        public Aluno(string nome)
        {
            Nome = nome;
            DataCadastro = DateTime.Now;

            //ValidateDomain(nome, dataCadastro);
        }

        public void ValidateDomain(string nome, string cpf)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Escreva um nome válido");
            DomainExceptionValidation.When(cpf.Length > 15, "CPF deve ter menos de 15 dígitos");
        }
    }
}


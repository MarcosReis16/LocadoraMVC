using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Cliente
    {
        [Key]
        public Guid IdCliente { get; set; }

        [Display(Name = "Nome")]
        public string NomeCliente { get; set; }

        [Display(Name = "CPF")]
        public string CPFCliente { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }
        public string Estado { get; set; }

        public List<Aluguel> Alugueis { get; set; }

        public Cliente()
        {

        }
        public Cliente(string nome, string cpf)
        {
            this.NomeCliente = nome;
            this.CPFCliente = cpf;
        }

    }
}

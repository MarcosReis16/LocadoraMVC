using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Auxiliares
{
    public enum Genero : int
    {
        Aventura,
        Romance,
        Ação,
        Suspense,
        Drama,
        Terror,
        Infantil,
        Documentário
    }

    public enum FaixaEtaria : int
    {
        Livre = 0,
        Dez = 10,
        Catorze = 14,
        Dezesseis = 16,
        Dezoito = 18
    }

    public struct Endereco
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }


    }
}

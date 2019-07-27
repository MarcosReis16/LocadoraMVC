using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Repository
{
    public interface IFilmeRepository
    {
        void CadastrarFilme(Filme filme);
        IEnumerable<Filme> RetornarBibliotecaFilmes();
        IEnumerable<Filme> RetornarFilmesSemEstoque();
        void EditarFilme(Filme filme);
        bool RemoverFilme(Guid id);

        Filme BuscarFilmePorId(Guid id);



    }
}

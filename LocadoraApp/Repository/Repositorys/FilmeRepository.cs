using Locadora.Contexto;
using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Locadora.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly LocadoraContext _context;

        public FilmeRepository(LocadoraContext context)
        {
            _context = context;
        }

        public void CadastrarFilme(Filme filme)
        {
            using (var transacao = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Filmes.Add(filme);
                    _context.SaveChanges();
                    transacao.Commit();

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }

        }

        public void EditarFilme(Filme filme)
        {
            using (var transacao = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Filmes.Update(filme);
                    _context.SaveChanges();
                    transacao.Commit();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }

        }

        public Filme BuscarFilmePorId(Guid id)
        {
            return _context.Filmes.FirstOrDefault(m => m.IdFilme == id);
        }


        public bool RemoverFilme(Guid id)
        {
            using (var transacao = _context.Database.BeginTransaction())
            {

                try
                {
                    var filme = _context.Filmes.First(m => m.IdFilme == id);
                    if (filme != null)
                    {
                        _context.Filmes.Remove(filme);
                        _context.SaveChanges();
                        transacao.Commit();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return false;

        }

        public IEnumerable<Filme> RetornarBibliotecaFilmes()
        {
            return _context.Filmes.OrderBy(m => m.NomeFilme).ThenBy(m => m.IdFilme).ToList();
        }

        public IEnumerable<Filme> RetornarFilmesSemEstoque()
        {
            return _context.Filmes.Where(m => m.QtdEstoque == 0).ToList();
        }




    }
}

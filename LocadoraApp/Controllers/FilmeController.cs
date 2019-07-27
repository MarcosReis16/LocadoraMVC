using Locadora.Models;
using Locadora.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Locadora.Controllers
{
    
    public class FilmeController : Controller
    {

        private readonly IFilmeRepository _repository;

        public FilmeController(IFilmeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult BuscarFilme()
        {
            try
            {
                var resultado = _repository.RetornarBibliotecaFilmes();

                if (resultado.Any())
                    return View(resultado);

                return NotFound();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult CriaFilme()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriaFilme(Guid id, [Bind("IdFilme, CodigoFilme, NomeFilme, GeneroFilme, FaixaEtariaFilme, ValorEmprestimo, QtdEstoque")] Filme filme)
        {
            if (id != filme.IdFilme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.CadastrarFilme(filme);
                }
                catch (Exception e)
                {
                    if (filme == null)
                    {
                        return RedirectToAction("BuscarFilme");
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }
                return RedirectToAction("BuscarFilme");
            }
            return View(filme);
        }

        public IActionResult EditaFilme(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var filme = _repository.BuscarFilmePorId(id);
                if (filme == null)
                {
                    return NotFound();
                }
                return View(filme);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditaFilme(Guid id, [Bind("IdFilme, CodigoFilme, NomeFilme, GeneroFilme, FaixaEtariaFilme, ValorEmprestimo, QtdEstoque")] Filme filme)
        {
            if (id != filme.IdFilme)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditarFilme(filme);
                }
                catch (Exception e)
                {
                    if (filme == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }
                return RedirectToAction("BuscarFilme");
            }
            return View(filme);
        }

        public IActionResult DetalheFilme(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var filme = _repository.BuscarFilmePorId(id);
                if (filme == null)
                {
                    return NotFound();
                }
                return View(filme);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult ApagaFilme(Guid id)
        {
            try
            {
                var filme = _repository.BuscarFilmePorId(id);

                if (filme != null)
                    return View(filme);

                return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost, ActionName("ApagaCliente")]
        [ValidateAntiForgeryToken]
        public IActionResult ApagaConfirmarFilme(Guid id)
        {
            _repository.RemoverFilme(id);
            return RedirectToAction(nameof(BuscarFilme));
        }
    }
}

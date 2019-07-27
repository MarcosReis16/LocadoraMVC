using Locadora.Models;
using Locadora.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Locadora.Controllers
{
    
    public class AluguelController : Controller
    {
        private readonly IAluguelRepository _repositoryAluguel;
        private readonly IFilmeRepository _repositoryFilme;


        public AluguelController(IAluguelRepository repositoryAluguel, IFilmeRepository repositoryFilme)
        {
            _repositoryAluguel = repositoryAluguel;
            _repositoryFilme = repositoryFilme;
        }

        // GET: api/Aluguel
        [HttpGet]
        public IActionResult BuscarAluguel()
        {
            try
            {
                var alugueis = _repositoryAluguel.ObterAlugueis();

                if (alugueis.Any())
                    return View(alugueis);

                return NotFound();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: api/Aluguel/5
        [HttpGet("{id}", Name = "GetAluguelCliente")]
        public IActionResult Get(Guid idCliente)
        {
            var alugueis = _repositoryAluguel.ObterAluguelPorCliente(idCliente);

            if (alugueis.Any())
                return Ok(alugueis);

            return NotFound(idCliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RealizaAluguel(Guid id, [Bind("IdAluguel, IdCliente, Filmes")] Aluguel aluguel, List<Filme> filmes)
        {
            try
            {
                aluguel.RealizarEmprestimo(filmes);
                aluguel = _repositoryAluguel.CriarAluguel(aluguel);
                filmes = aluguel.AluguelFilmes.Select(m => m.Filme).ToList();
                foreach (var filme in filmes)
                {
                    _repositoryFilme.EditarFilme(filme);
                }
                return View(aluguel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult RealizaAluguel()
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

        // PUT: api/Aluguel/5
        [HttpPut]
        public IActionResult Put(Guid idAluguel, bool renovar)
        {
            try
            {
                var aluguel = _repositoryAluguel.ObterAluguelPorId(idAluguel);
                if (!renovar)
                {
                    var filmes = aluguel.AluguelFilmes.Select(m => m.Filme).ToList();
                    aluguel.RealizarDevolucao(filmes);
                    foreach (var filme in filmes)
                    {
                        _repositoryFilme.EditarFilme(filme);
                    }

                }

                else
                    aluguel.RenovarEmprestimo();

                _repositoryAluguel.AlterarAluguel(aluguel);

                return Ok(aluguel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}

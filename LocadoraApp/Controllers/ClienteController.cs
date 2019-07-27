using Locadora.Auxiliares;
using Locadora.Models;
using Locadora.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Locadora.Controllers
{

    public class ClienteController : Controller
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult BuscarCliente()
        {
            try
            {
                var resultado = _repository.RetornaBibliotecaClientes();

                if (resultado.Any())
                    return View(resultado);

                return NotFound();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult CriaCliente()
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
        public IActionResult CriaCliente(Guid id, [Bind("IdCliente, NomeCliente, CPFCliente, CEP, Endereco, Bairro, Cidade, Estado")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.CadastraCliente(cliente);
                }
                catch (Exception e)
                {
                    if (cliente == null)
                    {
                        return RedirectToAction("BuscarCliente");
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }
                return RedirectToAction("BuscarCliente");
            }
            return View(cliente);
        }

        public IActionResult EditaCliente(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var cliente = _repository.RetornaClientePorId(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult DetalheCliente(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var cliente = _repository.RetornaClientePorId(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditaCliente(Guid id, [Bind("IdCliente, NomeCliente, CPFCliente, CEP, Endereco, Bairro, Cidade, Estado")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditaCliente(cliente);
                }
                catch (Exception e)
                {
                    if (cliente == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }
                return RedirectToAction("BuscarCliente");
            }
            return View(cliente);
        }



        
        public IActionResult ApagaCliente(Guid id)
        {
            try
            {
                var cliente = _repository.RetornaClientePorId(id);

                if (cliente != null)
                    return View(cliente);

                return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost, ActionName("ApagaCliente")]
        [ValidateAntiForgeryToken]
        public IActionResult ApagaConfirmarCliente(Guid id)
        {
            _repository.RemoveCliente(id);
            return RedirectToAction(nameof(BuscarCliente));
        }

        public JsonResult BuscaCEP(string cep)
        {
            var url = $"http://viacep.com.br/ws/{cep}/json/";
            WebClient client = new WebClient();

            try
            {
                var endereco = JsonConvert.DeserializeObject<Endereco>(client.DownloadString(url));
                return Json(endereco);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

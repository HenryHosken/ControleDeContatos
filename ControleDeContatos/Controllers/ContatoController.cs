using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _ContatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _ContatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _ContatoRepositorio.BuscarTodos();
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ContatoModel contato = _ContatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _ContatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _ContatoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ContatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ContatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch(System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

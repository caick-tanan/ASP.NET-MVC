using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;

        public SellersController(SellerService sellerService, DepartamentService departamentService) //injeção de dependência
        {
            _sellerService = sellerService;
            _departamentService = departamentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //vai retornar uma lista de service
            return View(list);
        }

        public IActionResult Create() //CRIAÇÃO DO MÉTODO CREATE
        {
            var departaments = _departamentService.FindAll(); //BUSCA NO BD todos os departamentos
            var viewModel = new SellerFormViewModel { Departaments = departaments };
            return View(viewModel); //retornando a view
        }

        [HttpPost] //o Post serve para enviar as info para o BD
        [ValidateAntiForgeryToken] //prevenção de ataques enquanto eu estiver autenticado
        public IActionResult Create(Seller seller)//cria em Seller um novo Seller
        {
            if (!ModelState.IsValid) //o modelo foi validado ?
            {
                var departaments = _departamentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel); //caso não seja validado retorna a propia view enquanto o usuário não preencher corretamente o formulário, caso o JavaScript esteja desativado na página web
            }
            _sellerService.Insert(seller); //inseriou a pessoa
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }
            var obj = _sellerService.FindyById(id.Value); //busquei no BD
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not providev" });
            }
            var obj = _sellerService.FindyById(id.Value); //busquei no BD
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id) //função para editar
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindyById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Departament> departaments = _departamentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) //o modelo foi validado ?
            {
                var departaments = _departamentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel); //caso não seja validado retorna a propia view enquanto o usuário não preencher corretamente o formulário, caso o JavaScript esteja desativado na página web
            }
            if (id != seller.Id)
            {
                RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}

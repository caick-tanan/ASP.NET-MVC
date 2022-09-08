using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) //injeção de dependência
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll(); //vai retornar uma lista de service
            return View(list);
        }

        public IActionResult Create() //CRIAÇÃO DO MÉTODO CREATE
        {
            return View(); //retornando a view
        }

        [HttpPost] //o Post serve para enviar as info para o BD
        [ValidateAntiForgeryToken] //prevenção de ataques enquanto eu estiver autenticado
        public IActionResult Create(Seller seller)//cria em Seller um novo Seller
        {
            _sellerService.Insert(seller); //inseriou a pessoa
            return RedirectToAction(nameof(Index)); 
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;

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
            var viewModel = new SellerFormViewModel { Departaments = departaments};
            return View(viewModel); //retornando a view
        }

        [HttpPost] //o Post serve para enviar as info para o BD
        [ValidateAntiForgeryToken] //prevenção de ataques enquanto eu estiver autenticado
        public IActionResult Create(Seller seller)//cria em Seller um novo Seller
        {
            _sellerService.Insert(seller); //inseriou a pessoa
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindyById(id.Value); //busquei no BD
            if (id == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

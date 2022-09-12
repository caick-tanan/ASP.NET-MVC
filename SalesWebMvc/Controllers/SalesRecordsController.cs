using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SalesWebMvc.Services;


namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); //caso o usuário nao informe uma data mínima, retornará a partir do dia 1 de janeiro do ano em que ele estiver
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now; //vai retorna o valor máximo atual
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); //passando os dados na forma do viewData
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate); //vai pegar o método FindByDate, com as datas mínimas e máximas
            return View(result);
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}

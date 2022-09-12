using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //essa declaração pega o SalesRecord que é do tipo Dbset e contruir para mim um objeto result do tipo Iquarible
            if (minDate.HasValue) //Caso o minDate possua valor
            {
                result = result.Where(x => x.Date >= minDate.Value); //vai pegar o objeto que foi contruido por meio do link e comparar com o minDate
            }
            if (maxDate.HasValue) //Caso o maxDate possua valor
            {
                result = result.Where(x => x.Date <= maxDate.Value); //vai pegar o objeto que foi contruido por meio do link e comparar com o minDate
            }
            return await result
                .Include(x => x.Seller) //faz um join com a tabela de Seller
                .Include(x => x.Seller.Departament) //faz um join com a tabela de Departament
                .OrderByDescending(x => x.Date) //Ordem Decrescente
                .ToListAsync(); //vai ordenar em formato de lista
        }
    }
}

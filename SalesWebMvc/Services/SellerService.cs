﻿using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //acessa a minha fonde de dados relacionado a tabela de vendedores e converter isso para uma lista
        }

        public void Insert(Seller obj) //inserir esse objeto no BD
        {
            _context.Add(obj); //adiciona o bojeto
            _context.SaveChanges(); //salva o bojeto
        }

        public Seller FindyById(int id)
        {
            return _context.Seller.Include(obj => obj.Departament).FirstOrDefault(obj => obj.Id == id); //vai pegar também o departamento da pessoa e mostrar na tela de detalhes
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}

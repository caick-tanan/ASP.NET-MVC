using System.Collections.Generic;
using System;
using System.Linq; //para acessar o where

namespace SalesWebMvc.Models

{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //o Tipo ICollection é um tipo mais genéric, aceitando List e HashSet

        public Departament() { }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final)); //pego cada vendedor da minha lista, chamando o TotalSales do vendedor em um período incial e final e fazendo a soma desse resultado para todos os vendedores do meu departamento
        }
    }
}

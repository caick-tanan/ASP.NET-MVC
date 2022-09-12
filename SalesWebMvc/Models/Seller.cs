using System; //para acessar o DateTime
using System.Collections.Generic; //para acessar o ICollection
using System.ComponentModel.DataAnnotations;
using System.Linq; //para acessar o where

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]//Gera um link para o email automaticamente
        public string Email { get; set; }

        [Display(Name = "Birth Date")] //SERVE PARA MUDAR O NOME NA WEB E NÃO DEIXAR JUNTO O NOME IGUAL NA VARIÁVEL
        [DataType(DataType.Date)] //Apenas ano, mês e dia
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //Define a data no formato dia, mês e ano
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //Define duas casas decimais no valor 
        public double BaseSalary { get; set; }

        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departament = departament;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); //total de vendas de um vendedor em um determinado intervalo de datas
        }
    }
}

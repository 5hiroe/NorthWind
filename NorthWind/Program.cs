using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NorthWindLibrary.Models;

namespace NorthWind
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new nothwindContext();
            
            //Exercice 1
            Separated("1");
            var employeeHiresIn1994 = context.Employees.Where(e => e.HireDate >= new DateTime(1994, 01, 01) && e.HireDate < new DateTime(1995, 01, 01) && e.City == "London").ToList();

            foreach (var employee in employeeHiresIn1994)
            {
                Console.WriteLine(employee.FirstName + " embauché le " + employee.HireDate + " Habitant à " + employee.City);
            }
    
            //Exercice 2
            Separated("2");
            var employeeRepresentative = context.Employees.Where(e => e.Title.Contains("Representative")).ToList();

            foreach (var employee in employeeRepresentative)
            {
                Console.WriteLine(employee.FirstName + " |  Titre : " + employee.Title);
            }
            
            //Exercice 3
            Separated("3");
            var averageSeaFood = context.Products.Where(p => p.Category.CategoryName == "Seafood")
                .Average(p => p.UnitPrice);
            
            Console.WriteLine("Prix Moyen : " + averageSeaFood + " €");
            
            //Exercice 4
            Separated("4");

            var orderAfter2May1996 = context.Orders.Where(o => o.OrderDate > new DateTime(1996, 5, 2)).Distinct().ToList();
            foreach (var order in orderAfter2May1996)
            {
                Console.WriteLine("Commande n° "+ order.OrderId + ", Date : " + order.OrderDate);
            }
            
            //Exercice 5
            Separated("5");
            var orders = context.Orderdetails.Where(o => o.UnitPrice > 230).Distinct().ToList();
            foreach (var order in orders)
            {
                Console.WriteLine("ID : " + order.OrderId);
            }
            
            
            //Exercice 6
            Separated("6");
            var totalProductsSeaFood = (from p in context.Set<Product>()
                from o in context.Set<Orderdetail>().Where(o => p.ProductId == o.ProductId).DefaultIfEmpty()
                select new
                {
                    p, o
                }).Where(o => o.o == null).ToList();

            foreach (var product in totalProductsSeaFood)
            {
                Console.WriteLine(product.p.ProductId + "     " + product.p.ProductName);
            }
             
            
            //Exercice 7
            Separated("7");
            var orderDetailsSelected = context.Orderdetails
                .Where(o => o.UnitPrice < 20 && o.Quantity > 40 && o.Discount >= 0.2 && o.Discount <= 0.3).ToList();

            foreach (var orderdetail in orderDetailsSelected)
            {
                Console.WriteLine("Order ID : "+ orderdetail.OrderId + ", Price : "+orderdetail.UnitPrice.ToString("N")+" €, Quantity : "+orderdetail.Quantity+", Discount : "+orderdetail.Discount);
            }
            
            //Exercice 8
            Separated("8");
            var employee120 = (from orderdetail in context.Orderdetails
                join order in context.Orders on orderdetail.OrderId equals order.OrderId
                where orderdetail.Quantity > 120
                select new
                {
                    order = order,
                    orderdetail = orderdetail
                });

            foreach (var employee in employee120)
            {
                Console.WriteLine(employee.order.Employee.FirstName + " " + employee.order.Employee.LastName);
            }
        }

        
        public static void Separated(string exercice)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Exercice " + exercice);
            Console.WriteLine("*************************************");
        }
    }
}
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
            var totalProductsSeaFood = context.Products.Select(product => new
            {
                product = product,
                CategoryName = product.Category.CategoryName
            }).Where(p => p.CategoryName == "Seafood").ToList();

            decimal totalprice = 0;
            int size = 0;
            foreach (var product in totalProductsSeaFood)
            {
                totalprice += product.product.UnitPrice.Value;
                size += 1;
            }
            Console.WriteLine(totalprice/size + " €");
            
            //Exercice 4
            Separated("4");

            var orderAfter2May1996 = context.Orders.Where(o => o.OrderDate > new DateTime(1996, 5, 2)).Distinct().ToList();
            foreach (var order in orderAfter2May1996)
            {
                Console.WriteLine("Commande n° "+ order.OrderId + ", Date : " + order.OrderDate);
            }
            
            //Exercice 5
            Separated("5");
            var orders = context.Orders.ToList();
            foreach (var order in orders)
            {
                var orderPrice = context.Orderdetails.Where(o => o.OrderId == order.OrderId).ToList();
                decimal price = 0;
                foreach (var orderdetail in orderPrice)
                {
                    price += orderdetail.UnitPrice;
                }

                if (price > 230)
                {
                    Console.WriteLine("Comamnde numéro "+order.OrderId);
                    price = 0;
                }
                price = 0;
            }
            
            //Exercice 6
            Separated("6");
            var orderedProducts = (from orderdetail in context.Orderdetails
                join product in context.Products on orderdetail.ProductId equals product.ProductId
                select new
                {
                    products = product
                });
            var productList = context.Products.ToList();

            bool isSame = false;
            foreach (var product in productList)
            {
                foreach (var products in orderedProducts)
                {
                    if (product.ProductId == products.products.ProductId)
                    {
                        isSame = true;
                    }
                }

                if (!isSame)
                {
                    Console.WriteLine(product.ProductName + "   " + product.ProductId);
                }

                isSame = false;
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
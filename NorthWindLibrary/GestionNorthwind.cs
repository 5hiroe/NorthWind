using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NorthWindLibrary.Models;

namespace NorthWindLibrary
{
    public class GestionNorthwind
    {
        private nothwindContext model = new nothwindContext();

        public List<Product> LoadProducts()
        {
            return model.Products.ToList();
        }

        public Product AddProduct(Product product)
        {
            model.Products.Add(product);
            return model.SaveChanges() > 0 ? product : null;
        }

        public List<Product> SearchProductsName(string name)
        {
            var liste = model.Products.Where(p => p.ProductName.Contains(name));
            return liste.ToList();
        }

        public Product SearchProductId(int id)
        {
            return model.Products.Find(id);
        }

        public bool EditProduct(Product product)
        {
            model.Entry(product).State = EntityState.Modified;
            return model.SaveChanges() > 0;
        }

        public bool RemoveProduct(int id)
        {
            Product p = SearchProductId(id);
            if (p == null)
            {
                return false;
            }
            return RemoveProduct(p);
        }

        public bool RemoveProduct(Product product)
        {
            if (product != null)
            {
                model.Products.Remove(product);
                return model.SaveChanges() > 0;
            }

            return false;
        }

        public void AddOrder(List<Product> listeACommander)
        {
            Order order = new Order();
            model.Orders.Add(order);
            foreach (var product in listeACommander)
            {
                decimal price = 0;
                if (product.UnitPrice != null)
                {
                    price = product.UnitPrice.Value;
                }
                else
                {
                    price = 1;
                }
                model.Orderdetails.Add(new Orderdetail
                    {
                        Product = product,
                        Discount = 0.1f,
                        Order = order,
                        Quantity = product.UnitsOnOrder,
                        UnitPrice = price,
                        OrderId = order.OrderId,
                        ProductId = product.ProductId
                    });
                Console.WriteLine(product.ProductName + " " + product.UnitsOnOrder);
            }
            model.SaveChanges();
        }
    }
}
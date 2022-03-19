using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    internal class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region AdminBilgileri
            AppUser adminUser = new AppUser();

            adminUser.UserName = "brk123";
            adminUser.Password = DantexCrypt.Crypt("123");
            adminUser.EMail = "burak_cevik76@hotmail.com.tr";
            adminUser.Role = ENTITIES.Enums.UserRole.Admin;

            context.AppUsers.Add(adminUser);
            context.SaveChanges();
            #endregion

            #region Category-Product Bilgileri

            for (int i = 0; i < 5; i++)
            {
                Category category = new Category();
                category.CategoryName = new Commerce("tr").Categories(1)[0];
                category.Description = new Lorem("tr").Sentence(7);
                category.Products = new List<Product>();

                for (int j = 0; j < 10; j++)
                {
                    Product product = new Product();
                    product.ProductName = new Commerce("tr").ProductName();
                    product.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    product.UnitsInStock = 50;

                    category.Products.Add(product);
                }
                context.Categories.Add(category);
                context.SaveChanges();
            }
            #endregion //Fake Datadır !!!
        }
    }
}

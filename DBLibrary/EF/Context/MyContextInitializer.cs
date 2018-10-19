using System.Data.Entity;
using System;
using DBLibrary.EF.Models;
using System.Collections.Generic;
using System.Windows;

namespace DBLibrary.EF.Context
{
    public class MyContextInitializer : DropCreateDatabaseIfModelChanges<ShopFilterEntity>
    {
        protected override void Seed(ShopFilterEntity context)
        {
            using(context = new ShopFilterEntity())
            {
                try
                {
                    Company c1 = new Company { Id = 1, Name = "Samsung" };
                    Company c2 = new Company { Id = 2, Name = "Apple" };
                    Company c3 = new Company { Id = 3, Name = "Xiaomi" };
                    context.Companies.AddRange(new List<Company> { c1, c2, c3 });

                    Phone p1 = new Phone { Name = "Samsung Galaxy Note 9", Price = 34999, ImagePath = "/Images/Samsung/product1.jpg", CompanyId = 1 };
                    Phone p2 = new Phone { Name = "Samsung Galaxy S9", Price = 29999, ImagePath = "/Images/Samsung/product2.jpg", CompanyId = 1 };
                    Phone p3 = new Phone { Name = "Apple Iphone XS Max 512GB", Price = 59000, ImagePath = "/Images/Apple/product1.jpg", CompanyId = 2 };
                    Phone p4 = new Phone { Name = "Apple Iphone XS Max 64GB", Price = 49999, ImagePath = "/Images/Apple/product2.jpg", CompanyId = 2 };
                    Phone p5 = new Phone { Name = "Xiaomi Mi 8", Price = 13999, ImagePath = "/Images/Xiaomi/product1.jpg", CompanyId = 3 };
                    Phone p6 = new Phone { Name = "Xiaomi Redmi Note 5", Price = 6399, ImagePath = "/Images/Xiaomi/product2.jpg", CompanyId = 3 };
                    context.Phones.AddRange(new List<Phone> { p1, p2, p3, p4, p5, p6 });

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }
    }
}

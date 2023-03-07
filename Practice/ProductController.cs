using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice
{
    public class ProductController : Controller
    {
        public IActionResult ProductsList() { 
            ProductDbContext prod = new ProductDbContext();
            var productlist= prod.Products.ToList();
            return View(productlist);
        }
        public IActionResult ProductDelete(int productId)
        {
            ProductDbContext prod = new ProductDbContext();
            var product = prod.Products.FirstOrDefault(x => x.ProductId == productId);
            if(product!= null)
            {
                prod.Products.Remove(product);
                prod.SaveChanges();
            }

            return RedirectToAction("ProductsList");
        }
        public IActionResult ProductDetails(int productId)
        {
            var dbContext = new ProductDbContext();
            Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            return View(product);

        }



        public IActionResult ProductEditor(int? productId = null)
        {
            ProductEditorModel model = new ProductEditorModel();

            if (productId.HasValue)
            {
                var dbContext = new ProductDbContext();

                Product product = dbContext.Products.FirstOrDefault(p => p.ProductId == productId);

                model.ProductName = product.ProductName;
                model.Description = product.Description;
                model.Price = product.Price;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductEditor(ProductEditorModel model)
        {
            if (ModelState.IsValid)
            {
                var dbContext = new ProductDbContext();

                var productObj = new Product();

                if (model.ProductId.HasValue)
                {
                    // its EDIT MODE, i.e user is updating the Product

                    productObj = dbContext.Products.FirstOrDefault(p => p.ProductId == model.ProductId);

                    productObj.ProductName = model.ProductName;
                    productObj.Price = model.Price;
                    productObj.Description = model.Description;

                    dbContext.Products.Update(productObj);
                }
                else
                {
                    // Add Mode, i.e user is adding new product 

                    productObj.ProductName = model.ProductName;
                    productObj.Description = model.Description;
                    productObj.Price = model.Price;

                    dbContext.Products.Add(productObj);
                }
                dbContext.SaveChanges();

                return RedirectToAction("ProductsList", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Product not saved, please fix errors and save again!");

                return View(model);
            }
        }
    }
}














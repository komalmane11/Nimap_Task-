using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInformation_CRUD_Operations.Models;
using System;


namespace ProductInformation_CRUD_Operations.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext mydbContext;
        public ProductController(AppDbContext context)
        {
            mydbContext = context;
        }

        [HttpGet]//first time form rendering
        public async Task<IActionResult> GetProduct()
        {
            var getProduct = await mydbContext.Products.OrderByDescending(_ => _.Id).ToListAsync();
            var model = new ProductModel
            {
                GetProduct = getProduct
            };
            return View("GetProduct", model);
        }
        [HttpGet]
        public IActionResult Create()//to render the empty form to add values
        {
            return View();
        }
        public async Task<IActionResult> Create(Product Products)
        {
            mydbContext.Products.Add(Products);
            await mydbContext.SaveChangesAsync();
            return RedirectToAction("GetProduct");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var Product = await mydbContext.Products.Where(_ => _.Id == Id).FirstOrDefaultAsync();
            if (Product == null)
            {
                return NotFound();
            }
            return View("Edit", Product);
        }
        public async Task<IActionResult> Edit(Product modeltoupdate)
        {
            mydbContext.Update(modeltoupdate);
            await mydbContext.SaveChangesAsync();
            return RedirectToAction("GetProduct");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var Product = await mydbContext.Products.
                Where(_ => _.Id == Id).FirstOrDefaultAsync();
            return View("Delete", Product);
        }
        public async Task<IActionResult> Delete(Product Productstodelete)
        {
            if (Productstodelete != null)
            {
                mydbContext.Products.Remove(Productstodelete);
                await mydbContext.SaveChangesAsync();
            }
            return RedirectToAction("GetProduct");
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            currentPageIndex=Math.Max(1, currentPageIndex);
            ProductModel model = GetProduct(currentPageIndex);
            return View(model);
        }
        private ProductModel GetProduct(int currentPage)
        {
            currentPage = Math.Max(1, currentPage);
            int maxRows = 10;
            ProductModel pro = new ProductModel();

            if(!this.mydbContext.Products.Any())
            {
                pro.PageCount = 0;
                pro.CurrentPageIndex = 0;
                pro.GetProduct = new List<Product>();
                return pro;
            }

            pro.GetProduct = this.mydbContext.Products
                                   .OrderBy(Product => Product.Id)
                                   .Skip((currentPage - 1) * maxRows)
                                   .Take(maxRows)
                                   .ToList();

            int totalProducts = this.mydbContext.Products.Count();
            double pageCount = (double)totalProducts / maxRows;
            pro.PageCount = (int)Math.Ceiling(pageCount);

            pro.CurrentPageIndex = currentPage;

            return pro;


        }


    }
}


    


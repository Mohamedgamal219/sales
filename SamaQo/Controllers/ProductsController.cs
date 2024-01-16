using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamaQo.Data;
using SamaQo.Models;

namespace SamaQo.Controllers
{
    public class ProductsController:Controller
    {
        private readonly IproductRepository repository;
        private readonly ApplicationDbContext context;

        public ProductsController(IproductRepository repository ,ApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        //public async Task<IActionResult> Index(string search="" , int supplierid=0)
        //{
        //    var all = await repository.GetAll(search, supplierid);
        //    var supp = await repository.GetAllSupplier();

        //    var DS = new DsProductSupplier
        //    {
        //        GetProducts = all,
        //        GetSuppliers = supp
        //    };
        //    return View(DS);
        //}

        public async Task< IActionResult> Index(string search = "")
        {
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");
            IEnumerable<Product> items = await context.Products.Include(e=>e.Supplier).ToListAsync();

            if (String.IsNullOrWhiteSpace(search))
            {
                items = items.Where(s => s.Name.ToLower().StartsWith(search)).ToList();
            }

            return View(items);
        }
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Product product)
        {

            await repository.AddProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await repository.detials(id);
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");

            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( Product product)
        {
            await repository.Update( product);
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await repository.detials(id);
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");

            return View(item);
        }
        [HttpPost ,ActionName("Delete")]
        public async Task<IActionResult> DeleteComfirm(int id)
        {
            ViewData["SupplierId"] = new SelectList(context.Suppliers, "Id", "SupplierName");
            var item = context.Products.FirstOrDefault(e => e.Id == id);
             context.Products.Remove(item);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

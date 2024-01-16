using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamaQo.Data;
using SamaQo.Models;

namespace SamaQo.Controllers
{
    public class StoragesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StoragesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IActionResult> Index(string search = "")
        {
            search = search.ToLower();
            var storage = await (from st in _dbContext.Storages
                                 join pr in _dbContext.Products
                                 on st.ProductId equals pr.Id
                                 where String.IsNullOrEmpty(search) || pr.Name.ToLower().StartsWith(search)
                                 select new Storage
                                 {
                                     Id = st.Id,
                                     ProductId = st.ProductId,
                                     DateOfStorage = st.DateOfStorage,
                                     Quantity = st.Quantity,
                                     ProdName = pr.Name
                                 }
                           ).ToListAsync();

           
            return View(storage);
        }

        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_dbContext.Products, "Id", "Name");

            var item = new Storage
            {
                DateOfStorage = DateTime.Now.Date
            };
            
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Quantity,DateOfStorage,ProductId")] Storage storage)
        {
           
            _dbContext.Storages.Add(storage);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details (int id)
        {
            var item = await (from st in _dbContext.Storages
                        join pd in _dbContext.Products
                        on st.ProductId equals pd.Id
                        where st.Id == id
                        select new Storage
                        {
                            Id = st.Id,
                            ProductId = st.ProductId,
                            Quantity = st.Quantity,
                            ProdName = pd.Name
                        }
                       ).FirstOrDefaultAsync();
           
            

            return View(item);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await (from st in _dbContext.Storages
                              join pd in _dbContext.Products
                              on st.ProductId equals pd.Id
                              where st.Id == id
                              select new Storage
                              {
                                  Id = st.Id,
                                  ProductId = st.ProductId,
                                  Quantity = st.Quantity,
                                  ProdName = pd.Name
                              }
                       ).FirstOrDefaultAsync();



            return View(item);
        }

        [HttpPost,ActionName("Delete")]
        
        public async Task<IActionResult> DeleteComfirm(int id)
        {
            var item =await _dbContext.Storages.FindAsync(id);
             _dbContext.Storages.Remove(item);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["ProductId"] = new SelectList(_dbContext.Products, "Id", "Name");
            var item = await (from st in _dbContext.Storages
                              join pd in _dbContext.Products
                              on st.ProductId equals pd.Id
                              where st.Id == id
                              select new Storage
                              {
                                  Id = st.Id,
                                  ProductId = st.ProductId,
                                  Quantity = st.Quantity,
                                  ProdName = pd.Name
                              }
                       ).FirstOrDefaultAsync();



            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Storage storage)
        {
            _dbContext.Storages.Update(storage);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}

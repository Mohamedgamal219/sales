using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamaQo.Data;
using SamaQo.Models;

namespace SamaQo.Controllers
{
    public class SalesSheetDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesSheetDetailsController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index(DateTime? search )
        
        {
            var items = await (from saleDet in _context.SalesSheetDetails
                              
                              
                               join produ in _context.Products
                               on saleDet.ProductId equals produ.Id
                               select new SalesSheetDetail
                               {
                                   Id = saleDet.Id,                                
                                   ProductId = produ.Id,
                                   ProName = produ.Name,
                                   UintPrice = produ.Price,
                                   Quantity = saleDet.Quantity,
                                   Total = (saleDet.Quantity * produ.Price),
                                   DateOfSale = saleDet.DateOfSale
                               }
                         ).ToListAsync();
            
            if(search.HasValue ) items = items.Where(e => e.DateOfSale.Date == search.Value.Date).ToList();
            return View(items);
        }

        public IActionResult Create()
        {  
            
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,DateOfSale,Quantity,ProductId")] SalesSheetDetail detail)
        {
            await _context.AddAsync(detail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        [Authorize]
        public async Task<IActionResult> AddToSale(int id)
        {
            if (id == null) throw new Exception("In Vaild");
            //var item =
            //             new SalesSheetDetail
            //             {
            //                 SalesSheetId = 1,
            //                 ProductId = id,
            //                 Quantity = 1,
            //                 DateOfSale = DateTime.Now

            //             };
            //var find = _context.SalesSheetDetails.FirstOrDefault(e => e.ProductId == id);

            //if(item.DateOfSale == DateTime.Now.Date && _context.SalesSheetDetails.Contains(find))
            //{
            //    find.Quantity += 1;
            //};
            //await _context.AddAsync(item);
            //await _context.SaveChangesAsync();
            //return RedirectToAction("Index");

            var item = _context.SalesSheetDetails.FirstOrDefault(e => e.ProductId == id);

            if(item != null)
            {
                item.Quantity += 1;
            }
            else
            {
                item = new SalesSheetDetail
                         {
                             ProductId = id,
                             Quantity = 1,
                             DateOfSale = DateTime.Now
                         };
                await _context.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete (int id)
        {
            var item = await _context.SalesSheetDetails.FirstOrDefaultAsync(e => e.Id == id);
            if(item.Quantity > 1)
            {
                item.Quantity -= 1;
            }
            else
            {
                _context.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Bill(int id)
        //{
        //    if (id == null) throw new Exception("In Vaild");
        //    var item = _context.SalesSheetDetails.FirstOrDefault(e => e.ProductId == id);

        //    if (item != null)
        //    {
        //        item.Quantity += 1;
        //    }
        //    else
        //    {
        //        item = new SalesSheetDetail
        //        {
        //            ProductId = id,
        //            Quantity = 1,
        //            DateOfSale = DateTime.Now
        //        };
        //        await _context.AddAsync(item);
        //    }
            
        //    return View(item);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Bill()
        //{
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}



    }
}

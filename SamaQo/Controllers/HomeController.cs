using Microsoft.AspNetCore.Mvc;
using SamaQo.Data;
using SamaQo.Models;
using System.Diagnostics;

namespace SamaQo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IproductRepository repository;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger,IproductRepository repository, ApplicationDbContext context)
        {
            _logger = logger;
            this.repository = repository;
            this.context = context;
        }
           

        public async Task<IActionResult> Index(string search = "", int supplierid = 0)
        {
            var all = await repository.GetAll(search, supplierid);
            var supp = await repository.GetAllSupplier();

            var DS = new DsProductSupplier
            {
                GetProducts = all,
                GetSuppliers = supp
            };
            return View(DS);
        }

        public IActionResult Branch()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
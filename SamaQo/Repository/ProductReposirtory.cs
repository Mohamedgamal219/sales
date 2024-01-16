using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamaQo.Data;
using SamaQo.Models;

namespace SamaQo.Repository
{
    public class ProductReposirtory:IproductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductReposirtory(ApplicationDbContext Db)
        {
            _db = Db;
        }

        public async Task<IEnumerable<Supplier>> GetAllSupplier()
        {
            var allSP = await _db.Suppliers.ToListAsync();
            return allSP;
        }


        public async Task<IEnumerable<Product>> GetAll(string search="" , int supplierid=0)
        {
            search = search.ToLower();

            var all = await (from produ in _db.Products
                       join supp in _db.Suppliers
                       on produ.SupplierId equals supp.Id
                       join st in _db.Storages
                       on produ.Id equals st.ProductId
                       where String.IsNullOrWhiteSpace(search) || produ.Name.ToLower().StartsWith(search)
                       select new Product
                       {
                           Id = produ.Id,
                           Name = produ.Name,
                           Price = produ.Price,
                           SupplierId = produ.SupplierId,
                           SupplierName = supp.SupplierName,
                           StorageQuantity = st.Quantity
                       }
                       ).ToListAsync();

            if (supplierid > 0)
            {
                all = all.Where(e => e.SupplierId == supplierid).ToList();
            }

            return all;
        }


        public async Task AddProduct([Bind("Id,Name,Price,SupplierId")]Product product)
        {
           
            _db.Products.Include(e => e.Supplier);
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> detials(int id)
        {
            var get = await _db.Products.Include(e=>e.Supplier)
                .FirstOrDefaultAsync(e=>e.Id == id);
            return get;
        }

        public async Task Update([Bind("Id,Name,Price,SupplierId")] Product product)
        {
             _db.Products.Update(product);
            await _db.SaveChangesAsync();   
        }

        public async Task Delete (int id)
        {
            var get = _db.Products.Find(id);
            _db.Products.Remove(get);
            await _db.SaveChangesAsync();
        }
    }
}

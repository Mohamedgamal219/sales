using Microsoft.AspNetCore.Mvc;
using SamaQo.Models;

namespace SamaQo
{
    public interface IproductRepository
    {
        Task<IEnumerable<Supplier>> GetAllSupplier();
        Task<IEnumerable<Product>> GetAll(string search, int supplierid);
        Task AddProduct([Bind("Id,Name,Price,ProduImage,SupplierId")] Product product);
        Task<Product> detials(int id);
        Task Update([Bind("Id,Name,Price,ProduImage,SupplierId")] Product product);
        Task Delete(int id);
    }
}
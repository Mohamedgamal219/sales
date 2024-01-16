using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SamaQo.Models;

namespace SamaQo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products {  get; set; }
        public DbSet<Supplier> Suppliers {  get; set; }
        public DbSet<UserMAG> UserMAGs {  get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<SalesSheetDetail> SalesSheetDetails { get; set; }
    }
}
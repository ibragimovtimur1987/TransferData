using Microsoft.EntityFrameworkCore;
using TransferData.DAL.Models;

namespace TransferData.DAL.EF
{
    public class TransferDataContext : DbContext
    {
        public TransferDataContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<ExcelModel> ExcelModel1 { get; set; }
        public DbSet<ExcelModel> ExcelModel2 { get; set; }
        public TransferDataContext(DbContextOptions<TransferDataContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}

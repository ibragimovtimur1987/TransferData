using Microsoft.EntityFrameworkCore;
namespace TransferData.DAL.EF
{
    public class TransferDataContext : DbContext
    {
        public TransferDataContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public TransferDataContext(DbContextOptions<TransferDataContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}

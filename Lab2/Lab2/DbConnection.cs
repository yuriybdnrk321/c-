using Microsoft.EntityFrameworkCore;


namespace Lab2
{
    
    public class DbConnection : DbContext
    {

        public DbConnection(DbContextOptions<DbConnection> options) : base(options) 
        {
            Database.EnsureCreated();
        }
       
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<InfoBook> InfoBooks { get; set; } = null!;
      
     
    }
}

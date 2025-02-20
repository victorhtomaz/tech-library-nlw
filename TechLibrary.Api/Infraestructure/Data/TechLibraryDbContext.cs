using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.Data;

public class TechLibraryDbContext(DbContextOptions<TechLibraryDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books {  get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}

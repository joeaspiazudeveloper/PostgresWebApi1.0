using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Models;

namespace PostgresWebApi.Data;

public class ApiDbContext: DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder) 
    { 
        builder.Entity<Carbrand>().HasData( 
            new Carbrand {Id = 1, Name = "Volkswagen", Description = "German car brand"} , 
            new Carbrand {Id = 2, Name = "Audi", Description = "German car brand"},
            new Carbrand {Id = 3, Name = "BMW", Description = "German car brand"}
        ); 
    }

    public DbSet<Carbrand> Carbrands {get; set;}
}
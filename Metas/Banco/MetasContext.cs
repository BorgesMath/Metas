using Metas.shared.Dados.Modelos;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Metas.Shared.Dados.Banco;

public class MetasContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{
    public DbSet<Meta> Metas { get; set; }
    public DbSet<Passos> Passos { get; set; }

    private readonly string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = METAS; Integrated Security = True; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

     
        modelBuilder.Entity<Passos>()
            .HasOne(p => p.Meta)
            .WithMany(m => m.Passos)  
            .HasForeignKey(p => p.MetaID)
            .OnDelete(DeleteBehavior.Cascade);  
    }
}

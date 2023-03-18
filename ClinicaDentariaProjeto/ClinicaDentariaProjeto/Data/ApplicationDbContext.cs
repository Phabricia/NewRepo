using ClinicaDentariaProjeto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaDentariaProjeto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
    }
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Consulta> Consultas { get; set; } = default!;
    }
}
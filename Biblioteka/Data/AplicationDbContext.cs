using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Konfiguracja OpenIddict w bazie danych
        builder.UseOpenIddict();
    }
}
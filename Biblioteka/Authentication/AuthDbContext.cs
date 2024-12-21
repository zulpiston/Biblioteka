using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

namespace Biblioteka.Authentication;

// Kontekst bazy danych dla OpenIddict
public class AuthDbContext : IdentityDbContext<IdentityUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) { }

    // Encje OpenIddict
    public DbSet<OpenIddictEntityFrameworkCoreApplication> Applications { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreAuthorization> Authorizations { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreScope> Scopes { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreToken> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Konfiguracja OpenIddict
        builder.UseOpenIddict();
    }
}

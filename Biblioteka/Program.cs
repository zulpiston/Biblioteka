using Biblioteka.Data;
using Biblioteka.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("BooksApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7164/"); 
});
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();


builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    // Integracja z OpenIddict
    options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        // U¿ycie Entity Framework Core dla OpenIddict
        options.UseEntityFrameworkCore()
               .UseDbContext<ApplicationDbContext>();
    })
    .AddServer(options =>
    {
        options.AllowAuthorizationCodeFlow(); // U¿ywamy Authorization Code Flow
        options.SetTokenEndpointUris("/connect/token");
        options.SetAuthorizationEndpointUris("/connect/authorize");

        // U¿ycie certyfikatu deweloperskiego do szyfrowania
        options.AddDevelopmentEncryptionCertificate(); // Dodanie certyfikatu deweloperskiego

        // Dodanie klucza do podpisywania tokenów
        options.AddEphemeralSigningKey(); // Tymczasowy klucz do podpisywania tokenów (do testów)

        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough();
    })
    .AddValidation(options =>
    {
        // Mo¿esz dodaæ opcje walidacji, jeœli masz zamiar walidowaæ tokeny
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Dodanie obs³ugi uwierzytelniania
app.UseAuthorization(); // Dodanie obs³ugi autoryzacji

app.UseAuthentication(); // Obs³uga uwierzytelniania
app.UseAuthorization();  // Obs³uga autoryzacji

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

var applicationManager = serviceProvider.GetRequiredService<OpenIddict.Abstractions.IOpenIddictApplicationManager>();

if (await applicationManager.FindByClientIdAsync("blazor-server") == null)
{
    await applicationManager.CreateAsync(new OpenIddict.Abstractions.OpenIddictApplicationDescriptor
    {
        ClientId = "blazor-server",
        RedirectUris = { new Uri("https://localhost:5001/signin-oidc") },
        Permissions =
        {
            OpenIddict.Abstractions.OpenIddictConstants.Permissions.Endpoints.Authorization,
            OpenIddict.Abstractions.OpenIddictConstants.Permissions.Endpoints.Token,
            OpenIddict.Abstractions.OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
            OpenIddict.Abstractions.OpenIddictConstants.Permissions.ResponseTypes.Code,
            OpenIddict.Abstractions.OpenIddictConstants.Permissions.Scopes.Profile
        }
    });
}


app.Run();

using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Makaan.CORE.Models;
public class User : IdentityUser
{
    public string FullName { get; set; } = null!;
}

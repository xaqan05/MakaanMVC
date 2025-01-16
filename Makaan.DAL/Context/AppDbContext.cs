using Makaan.CORE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Makaan.DAL.Context;
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Designation> Designations { get; set; }
}

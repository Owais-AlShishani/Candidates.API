using Candidates.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Candidates.API.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Candidate> Candidates { get; set; }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateService.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Job> Jobs { get; set; }
    }
}
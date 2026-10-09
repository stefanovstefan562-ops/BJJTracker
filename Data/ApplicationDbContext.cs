using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyMvcApp.Models;

namespace MyMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        public DbSet<SparringSession> SparringSessions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Academy> Academies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Practitioner>()
                .HasOne(p => p.Academy)
                .WithMany()
                .HasForeignKey(p => p.AcademyId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}


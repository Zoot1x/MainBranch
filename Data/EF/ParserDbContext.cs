

using Microsoft.EntityFrameworkCore;
using Project.Models.Parser;

namespace Project.Data.EF
{
    public class ParserDbContext : DbContext
    {
        public DbSet<Speciality>? Specialities { get; set; }
        public DbSet<Discipline>? Disciplines { get; set; }
        public DbSet<Semester>? Semesters { get; set; }

        public ParserDbContext(DbContextOptions<ParserDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.Entity<Speciality>()
                .HasMany(s => s.Disciplines)
                .WithOne(d => d.Speciality)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discipline>()
                .HasMany(s => s.Semesters)
                .WithOne(d => d.Discipline)
                .HasForeignKey(d => d.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Speciality>().ToTable("Specialities");

            modelBuilder.Entity<Discipline>().ToTable("Disciplines");

            modelBuilder.Entity<Semester>().ToTable("Semesters");

        }   
    }
}
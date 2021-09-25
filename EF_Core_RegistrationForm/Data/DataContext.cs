using EF_Core_RegistrationForm.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_RegistrationForm.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasMany(r => r.QuestionAnswers);
            modelBuilder.Entity<QuestionAnswer>()
                .HasKey(cb => new { cb.QuestionId, cb.AnswerId });
            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(cb => cb.Question)
                .WithMany()
                .HasForeignKey(cb => cb.QuestionId);
            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(cb => cb.Answer)
                .WithMany()
                .HasForeignKey(cb => cb.AnswerId);

        }
    }
}

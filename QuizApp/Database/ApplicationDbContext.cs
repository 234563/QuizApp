using Microsoft.EntityFrameworkCore;
using QuizApp.Enities;

namespace QuizApp
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        #endregion

       
        /// <summary>
        /// Db Set for ApplicationUser Entities 
        /// </summary>
        public DbSet<User> ApplicationUsers { get; set; }
        /// <summary>
        /// Db Set for Questions Entities 
        /// </summary>
        public DbSet<Question> Questions { get; set; }
        /// <summary>
        // Db Set for UserQuestions Entities 
        // </summary>
        public DbSet<UserQuestion> UserQuestions { get; set; }

        /// <summary>
        // Db Set for InternetNetworks Entities 
        // </summary>
        public DbSet<InternetNetwork> InternetNetworks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserQuestion>()
            .HasKey(uq => new { uq.UserId, uq.QuestionId });

            modelBuilder.Entity<UserQuestion>()
                .HasOne(uq => uq.User)
                .WithMany(u => u.QuestionsAnswered)
                .HasForeignKey(uq => uq.UserId);

            modelBuilder.Entity<UserQuestion>()
                .HasOne(uq => uq.Question)
                .WithMany(q => q.UsersWhoAnswered)
                .HasForeignKey(uq => uq.QuestionId);
        }
    }
}

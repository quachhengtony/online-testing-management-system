using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace BusinessObjects.DbContexts
{
    public partial class OnlineTestingManagementSystemDbContext : DbContext
    {
        public OnlineTestingManagementSystemDbContext()
        {
            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            this.Database.SetConnectionString(config["ConnectionStrings:OnlineTestingManagementSystemDb"]);
        }

        public OnlineTestingManagementSystemDbContext(DbContextOptions<OnlineTestingManagementSystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }
        public virtual DbSet<QuestionGroupQuestion> QuestionGroupQuestions { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestCategory> TestCategories { get; set; }
        public virtual DbSet<TestCreator> TestCreators { get; set; }
        public virtual DbSet<TestQuestionGroup> TestQuestionGroups { get; set; }
        public virtual DbSet<TestTaker> TestTakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-GH1MMGQJ\\SQLEXPRESS01;Database=OnlineTestingManagementSystemDb;Uid=sa;Password=password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Answer__Question__46E78A0C");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.QuestionCreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.QuestionCategory)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionCategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Question__Questi__45F365D3");

                entity.HasOne(d => d.QuestionCreator)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionCreatorId)
                    .HasConstraintName("FK__Question__Questi__49C3F6B7");
            });

            modelBuilder.Entity<QuestionCategory>(entity =>
            {
                entity.ToTable("QuestionCategory");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                entity.ToTable("QuestionGroup");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.QuestionGroupCreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.QuestionGroupCreator)
                    .WithMany(p => p.QuestionGroups)
                    .HasForeignKey(d => d.QuestionGroupCreatorId)
                    .HasConstraintName("FK__QuestionG__Quest__48CFD27E");
            });

            modelBuilder.Entity<QuestionGroupQuestion>(entity =>
            {
                entity.HasKey(e => new { e.QuestionGroupId, e.QuestionId })
                    .HasName("PK__Question__C718677389274084");

                entity.ToTable("QuestionGroupQuestion");

                entity.Property(e => e.QuestionGroupId).HasMaxLength(25);

                entity.Property(e => e.QuestionId).HasMaxLength(25);

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.QuestionGroupQuestions)
                    .HasForeignKey(d => d.QuestionGroupId)
                    .HasConstraintName("FK_QuestionGroupQuestion_QuestionGroup");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionGroupQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionGroupQuestion_Question");
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.ToTable("Submission");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Feedback).HasMaxLength(100);

                entity.Property(e => e.GradedDate).HasColumnType("datetime");

                entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubmittedDate).HasColumnType("datetime");

                entity.Property(e => e.TestId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.TestTakerId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__Submissio__TestI__4AB81AF0");

                entity.HasOne(d => d.TestTaker)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.TestTakerId)
                    .HasConstraintName("FK__Submissio__TestT__4BAC3F29");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.GradeFinalizationDate).HasColumnType("datetime");

                entity.Property(e => e.GradeReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.KeyCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TestCreatorId)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.TestCategory)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TestCategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Test__TestCatego__4CA06362");

                entity.HasOne(d => d.TestCreator)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TestCreatorId)
                    .HasConstraintName("FK__Test__TestCreato__47DBAE45");
            });

            modelBuilder.Entity<TestCategory>(entity =>
            {
                entity.ToTable("TestCategory");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestCreator>(entity =>
            {
                entity.ToTable("TestCreator");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<TestQuestionGroup>(entity =>
            {
                entity.HasKey(e => new { e.QuestionGroupId, e.TestId })
                    .HasName("PK__TestQues__0F08529F6A70EBEC");

                entity.ToTable("TestQuestionGroup");

                entity.Property(e => e.QuestionGroupId).HasMaxLength(25);

                entity.Property(e => e.TestId).HasMaxLength(25);

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.TestQuestionGroups)
                    .HasForeignKey(d => d.QuestionGroupId)
                    .HasConstraintName("FK_TestQuestionGroup_QuestionGroup");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestQuestionGroups)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestQuestionGroup_Test");
            });

            modelBuilder.Entity<TestTaker>(entity =>
            {
                entity.ToTable("TestTaker");

                entity.Property(e => e.Id).HasMaxLength(25);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

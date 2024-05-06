using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlantsDetection.Models
{
    public partial class PlantsDetectionContext : DbContext
    {
        public PlantsDetectionContext()
        {
        }

        public PlantsDetectionContext(DbContextOptions<PlantsDetectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<DetectDisease> DetectDiseases { get; set; } = null!;
        public virtual DbSet<ModelImage> ModelImages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PlantsDetection;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.ComId);

                entity.ToTable("Comment");

                entity.Property(e => e.ComId).HasColumnName("ComID");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DetectDisease>(entity =>
            {
                entity.HasKey(e => e.Did);

                entity.ToTable("DetectDisease");

                entity.Property(e => e.Did).HasColumnName("DID");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiseaseType).HasMaxLength(50);

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.DetectDiseases)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_DetectDisease_ModelImage");
            });

            modelBuilder.Entity<ModelImage>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.ToTable("ModelImage");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ModelImages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ModelImage_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Describtion).HasMaxLength(500);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Report_User");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientName).HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Token1)
                    .HasMaxLength(50)
                    .HasColumnName("Token");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tokens_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(14);

                entity.Property(e => e.StreetName).HasMaxLength(50);

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasMany(d => d.Coms)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserDisLikeComment",
                        l => l.HasOne<Comment>().WithMany().HasForeignKey("ComId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserDisLikeComment_Comment"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserDisLikeComment_User"),
                        j =>
                        {
                            j.HasKey("UserId", "ComId");

                            j.ToTable("UserDisLikeComment");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("ComId").HasColumnName("ComID");
                        });

                entity.HasMany(d => d.ComsNavigation)
                    .WithMany(p => p.UsersNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserLikeComment",
                        l => l.HasOne<Comment>().WithMany().HasForeignKey("ComId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserLikeComment_Comment"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserLikeComment_User"),
                        j =>
                        {
                            j.HasKey("UserId", "ComId");

                            j.ToTable("UserLikeComment");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("ComId").HasColumnName("ComID");
                        });

                entity.HasMany(d => d.Posts)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserDisLikePost",
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserDisLikePost_Post"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserDisLikePost_User"),
                        j =>
                        {
                            j.HasKey("UserId", "PostId");

                            j.ToTable("UserDisLikePost");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        });

                entity.HasMany(d => d.PostsNavigation)
                    .WithMany(p => p.UsersNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserLikePost",
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserLikePost_Post"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserLikePost_User"),
                        j =>
                        {
                            j.HasKey("UserId", "PostId");

                            j.ToTable("UserLikePost");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("PostId").HasColumnName("PostID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

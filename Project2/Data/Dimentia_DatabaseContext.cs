using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project2.Models;

namespace Project2.Data
{
    public partial class Dimentia_DatabaseContext : DbContext
    {
        public Dimentia_DatabaseContext()
        {
        }

        public Dimentia_DatabaseContext(DbContextOptions<Dimentia_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<EmployeeCost> EmployeeCost { get; set; }
        public virtual DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public virtual DbSet<EmployeeHistory> EmployeeHistory { get; set; }
        public virtual DbSet<EmployeeInfo> EmployeeInfo { get; set; }
        public virtual DbSet<EmployeeReview> EmployeeReview { get; set; }
        public virtual DbSet<JobInformation> JobInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Initial Catalog=Dimentia_Database;Data Source=LAPTOP-08JT0V2M;Trusted_Connection =true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<EmployeeCost>(entity =>
            {
                entity.HasKey(e => e.CostId);

                entity.Property(e => e.CostId).HasColumnName("Cost_id");

                entity.Property(e => e.MonthlyIncome).HasColumnType("money");

                entity.Property(e => e.OverTime).HasMaxLength(50);

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeCost)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EmployeeCost_EmployeeDetails");
            });

            modelBuilder.Entity<EmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber);

                entity.Property(e => e.EmployeeNumber).ValueGeneratedNever();

                entity.Property(e => e.Attrition).HasMaxLength(50);

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.DetailId)
                    .HasColumnName("Detail_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EducationField).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);

                entity.Property(e => e.Over18)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<EmployeeHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.HistoryId).HasColumnName("History_id");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeHistory)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EmployeeHistory_EmployeeDetails");
            });

            modelBuilder.Entity<EmployeeInfo>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("Employee_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostId).HasColumnName("Cost_id");

                entity.Property(e => e.DetailId).HasColumnName("Detail_id");

                entity.Property(e => e.HistoryId).HasColumnName("History_id");

                entity.Property(e => e.JobId).HasColumnName("Job_id");

                entity.Property(e => e.ReviewId).HasColumnName("Review_id");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeInfo)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .HasConstraintName("FK_EmployeeInfo_EmployeeDetails");
            });

            modelBuilder.Entity<EmployeeReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.Property(e => e.ReviewId).HasColumnName("Review_id");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.EmployeeReview)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EmployeeReview_EmployeeDetails");
            });

            modelBuilder.Entity<JobInformation>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobId).HasColumnName("Job_id");

                entity.Property(e => e.BusinessTravel).HasMaxLength(50);

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.JobRole).HasMaxLength(50);

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.JobInformation)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_JobInformation_EmployeeDetails");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LoanApplicationService.Models
{
    public partial class LoanApplicationSystemContext : DbContext
    {
        public LoanApplicationSystemContext()
        {
        }

        public LoanApplicationSystemContext(DbContextOptions<LoanApplicationSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressDetail> AddressDetails { get; set; }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=LoanApplicationDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressDetail>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.AddressLine)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.AddressDetails)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AddressDetails_Applicant");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.AddressDetails)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK_AddressDetails_Business");
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.ToTable("Applicant");

                entity.HasIndex(e => e.Email, "Email");

                entity.HasIndex(e => e.FirstName, "FirstName");

                entity.HasIndex(e => e.LastName, "LastName");

                entity.HasIndex(e => e.MiddleName, "MiddleName");

                entity.Property(e => e.DateofBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.HasIndex(e => e.Title, "Title");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WebsiteURL");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Business_Applicant");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.HasIndex(e => e.AmountRequested, "AmontRequested");

                entity.HasIndex(e => e.CreditRating, "CreditRating");

                entity.HasIndex(e => e.DateApplied, "DateApplied");

                entity.Property(e => e.AmountRequested).HasColumnType("money");

                entity.Property(e => e.Apr).HasColumnName("APR");

                entity.Property(e => e.DateApplied).HasColumnType("date");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Loan_Applicant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

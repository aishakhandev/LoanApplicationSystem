//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//#nullable disable

//namespace LoanApplicationService.Models
//{
//    public partial class LoanAppDBContext : DbContext
//    {
//        public LoanAppDBContext()
//        {
//        }



//        public LoanAppDBContext(DbContextOptions<LoanAppDBContext> options)
//            : base(options)
//        {
//        }

//        protected  void SeedData(ModelBuilder modelBuilder)
//        {
//            List<Loan> fakeApplicants = new List<Loan>();

//            Random randNo = new Random();
//            for (int counter = 1; counter <= 2; counter++)
//            {
//                fakeApplicants.Add(
                    
//                        new Loan()
//                        {
//                             Applicant= new Applicant
//                             {
//                                 ApplicantId = 1,
//                                    FirstName = "first" + randNo.Next(100),
//                                    LastName = "last" + randNo.Next(100),
//                                    Phone = randNo.Next(10000000).ToString(),
//                                    Nationality = "USA",
//                                    DateofBirth = new DateTime(1975, 12, 12),
                                    

//                             },

//                            AmountRequested = randNo.Next(100000),
//                            CreditRating = 10,
//                            LoanId= 1,
//                             NoOfMonthsToPayback = 0,
//                             NoOfYearsToPayback= 10,
//                             NoOfOutstaningDebts=0,
//                             Apr= 7,
//                             DateApplied = DateTime.Now,
//                             LatePaymentsin5years=0,
//                             RiskRating= randNo.Next(5),
//                             Business= new Business
//                             {
//                                 BusinessId=1,
//                                 Title="Random Business",
//                                  Email ="test@ab.com",
//                                  Phone="12323",
//                                  WebsiteUrl ="https://test.abc.com",
//                                  BusinessType = new BusinessType
//                                  {
//                                  BusinessTypeId = 1,
//                                  BusinessType1= "Test"+ counter
//                                  },
//                                  Address = new AddressDetail
//                                  {
//                                    AddressLine="street " + counter ,
//                                    City= "Brooklyn",
//                                    State="NY",
//                                    Country="NY",
//                                    ZipCode="11223",
//                                    AddressId = 1

//                                  }
//                             }


//                        }

                    
//                    );
//            }


//            modelBuilder.Entity<Loan>().HasData
//                (
//                fakeApplicants

//                );
//         }

//        public virtual DbSet<AddressDetail> AddressDetails { get; set; }
//        public virtual DbSet<Applicant> Applicants { get; set; }
//        public virtual DbSet<Business> Businesses { get; set; }
//        public virtual DbSet<BusinessType> BusinessTypes { get; set; }
//        public virtual DbSet<Loan> Loans { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Name=LoanApplicationDB");
//            }
//        }

//        protected  override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<AddressDetail>(entity =>
//            {
//                entity.HasKey(e => e.AddressId);

//                entity.Property(e => e.AddressId).ValueGeneratedNever();

//                entity.Property(e => e.AddressLine)
//                    .HasMaxLength(250)
//                    .IsUnicode(false)
//                    .HasColumnName("addressLine");

//                entity.Property(e => e.City)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Country)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.State)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.ZipCode)
//                    .HasMaxLength(10)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<Applicant>(entity =>
//            {
//                entity.ToTable("Applicant");

//                entity.Property(e => e.ApplicantId).ValueGeneratedNever();

//                entity.Property(e => e.DateofBirth).HasColumnType("date");

//                entity.Property(e => e.Email)
//                    .HasMaxLength(10)
//                    .IsFixedLength(true);

//                entity.Property(e => e.FirstName)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Gender)
//                    .HasMaxLength(1)
//                    .IsUnicode(false)
//                    .IsFixedLength(true);

//                entity.Property(e => e.LastName)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.MiddleName).HasMaxLength(50);

//                entity.Property(e => e.Nationality)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Phone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.Address)
//                    .WithMany(p => p.Applicants)
//                    .HasForeignKey(d => d.AddressId)
//                    .HasConstraintName("FK_Applicant_AddressDetails");
//            });

//            modelBuilder.Entity<Business>(entity =>
//            {
//                entity.ToTable("Business");

//                entity.Property(e => e.BusinessId).ValueGeneratedNever();

//                entity.Property(e => e.Email)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Phone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Title)
//                    .HasMaxLength(150)
//                    .IsUnicode(false);

//                entity.Property(e => e.WebsiteUrl)
//                    .HasMaxLength(50)
//                    .IsUnicode(false)
//                    .HasColumnName("WebsiteURL");

//                entity.HasOne(d => d.Address)
//                    .WithMany(p => p.Businesses)
//                    .HasForeignKey(d => d.AddressId)
//                    .HasConstraintName("FK_Business_AddressDetails");

//                entity.HasOne(d => d.BusinessType)
//                    .WithMany(p => p.Businesses)
//                    .HasForeignKey(d => d.BusinessTypeId)
//                    .HasConstraintName("FK_Business_BusinessType");
//            });

//            modelBuilder.Entity<BusinessType>(entity =>
//            {
//                entity.ToTable("BusinessType");

//                entity.Property(e => e.BusinessTypeId).ValueGeneratedNever();

//                entity.Property(e => e.BusinessType1)
//                    .HasMaxLength(50)
//                    .IsUnicode(false)
//                    .HasColumnName("BusinessType");
//            });

//            modelBuilder.Entity<Loan>(entity =>
//            {
//                entity.ToTable("Loan");

//                entity.Property(e => e.LoanId).ValueGeneratedNever();

//                entity.Property(e => e.AmountRequested).HasColumnType("money");

//                entity.Property(e => e.Apr).HasColumnName("APR");

//                entity.Property(e => e.DateApplied).HasColumnType("date");

//                entity.HasOne(d => d.Applicant)
//                    .WithMany(p => p.Loans)
//                    .HasForeignKey(d => d.ApplicantId)
//                    .HasConstraintName("FK_Loan_Applicant");

//                entity.HasOne(d => d.Business)
//                    .WithMany(p => p.Loans)
//                    .HasForeignKey(d => d.BusinessId)
//                    .HasConstraintName("FK_Loan_Business");
//            });

////SeedData(modelBuilder);
//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}

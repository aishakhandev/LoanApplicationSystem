using LoanApplicationContracts;
using LoanApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applicant = LoanApplicationService.Models.Applicant;
using Loan = LoanApplicationService.Models.Loan;
using Business = LoanApplicationService.Models.Business;
using AddressDetail = LoanApplicationService.Models.AddressDetail;

namespace LoanApplicationService.Data
{

    public static class DbInitializer
    {
      //  This is to Seed data in database
        public static void Initialize(LoanApplicationSystemContext context)
        {
            context.Database.EnsureCreated();

            if (context.Applicants.Any())
            {
                return;   // DB has been seeded already
            }

            Random randNo = new Random();

            Applicant applicant = new Applicant();
            Business business = new Business();
            Loan loan = new Loan();
            AddressDetail addr = new AddressDetail();


            for (int i = 1; i <= 10; i++)
            {
                applicant = new Applicant
                {
                    FirstName = "first" + randNo.Next(100),
                    LastName = "last" + randNo.Next(100),
                    Phone = randNo.Next(10000000).ToString(),
                    Nationality = "USA",
                    DateofBirth = new DateTime(1990, 12, 12),
                    Gender = "M",
                    Email= "first" + randNo.Next(100) +"@test.com",
                    MiddleName = "Middle" + randNo.Next(100)
                };
                loan = new Loan
                {
                    AmountRequested = randNo.Next(100000),
                    CreditRating = (short)randNo.Next(600, 750),
                    NoOfMonthsToPayback = 0,
                    NoOfYearsToPayback = 10,
                    NoOfOutstaningDebts = 0,
                    Apr = 7,
                    TotalOutstandingDebt = 0,
                    DateApplied = DateTime.Now,
                    LatePaymentsin5years = 0,
                    RiskRating = randNo.Next(1, 3)
                };

                business = new Business
                {
                    Title = "Company No" + randNo.Next(100),
                    Email = "sampleemail" + randNo.Next(10) + "@sample.com",
                  //  BusinessTypeId = 1,
                    Phone = randNo.Next(10000000).ToString(),
                    WebsiteUrl = "www.company.com"
                };

                addr = new AddressDetail
                {
                    City = "JC",
                    Country = "USA",
                    State = "NJ",
                    AddressLine = "apartment 1",
                    ZipCode = "12345"
                };


                applicant.Businesses.Add(business);
                applicant.AddressDetails.Add(addr);
                applicant.Loans.Add(loan);

                context.Applicants.Add(applicant);

            }
            context.SaveChanges();

          
        }
    }

}

using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract = LoanApplicationContracts;
using EFModel = LoanApplicationService.Models;

namespace LoanApplicationService.Models
{
    public class ApplicantRepository : IApplicantRepository
    {
        private EFModel.LoanApplicationSystemContext _dbContext;
        private ILogger<ApplicantRepository> _logger;

       public ApplicantRepository(EFModel.LoanApplicationSystemContext dbContext, ILogger<ApplicantRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public IEnumerable<Applicant> GetAllApplicants()
        {
            _logger.LogInformation("GetAllApplicants");
           return _dbContext.Applicants.OrderByDescending(x => x.ApplicantId).ToList();

        }

        public Applicant CreateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails)
        {
            var mapperConfig = new MapperConfiguration
             (cfg =>
             {
                 cfg.CreateMap<Contract.Applicant, EFModel.Applicant>();
                 cfg.CreateMap<Contract.Business, EFModel.Business>();
                 cfg.CreateMap<Contract.AddressDetail, EFModel.AddressDetail>();
                 cfg.CreateMap<Contract.Applicant, EFModel.AddressDetail>();
                 cfg.CreateMap<Contract.Loan, EFModel.Loan>();
             });

            IMapper iMapper = mapperConfig.CreateMapper();

            EFModel.Applicant applicantToSave = iMapper.Map<Contract.Applicant, EFModel.Applicant>(loanApplicationDetails.applicant);
            EFModel.Loan loanToSave = iMapper.Map<Contract.Loan, EFModel.Loan>(loanApplicationDetails.loan);
            EFModel.Business businessToSave = iMapper.Map<Contract.Business, EFModel.Business>(loanApplicationDetails.business);
            EFModel.AddressDetail addrToSave = iMapper.Map<Contract.Applicant, EFModel.AddressDetail>(loanApplicationDetails.applicant);

            try
            {

                applicantToSave.Businesses.Add(businessToSave);
                applicantToSave.AddressDetails.Add(addrToSave);
                applicantToSave.Loans.Add(loanToSave);


                _dbContext.Applicants.Add(applicantToSave);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in SaveLoanApplication - Reporsitory Method: {ex}");
            }
            return applicantToSave;

        }
        public Applicant UpdateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails)
        {
            var mapperConfig = new MapperConfiguration
              (cfg =>
              {
                  cfg.CreateMap<Contract.Applicant, EFModel.Applicant>();
                  cfg.CreateMap<Contract.Business, EFModel.Business>();
                  cfg.CreateMap<Contract.AddressDetail, EFModel.AddressDetail>();
                  cfg.CreateMap<Contract.Applicant, EFModel.AddressDetail>();
                  cfg.CreateMap<Contract.Loan, EFModel.Loan>();
              });

            IMapper iMapper = mapperConfig.CreateMapper();

            EFModel.Applicant applicantToSave = iMapper.Map<Contract.Applicant, EFModel.Applicant>(loanApplicationDetails.applicant);
            EFModel.Loan loanToSave = iMapper.Map<Contract.Loan, EFModel.Loan>(loanApplicationDetails.loan);
            EFModel.Business businessToSave = iMapper.Map<Contract.Business, EFModel.Business>(loanApplicationDetails.business);
            EFModel.AddressDetail addrToSave = iMapper.Map<Contract.Applicant, EFModel.AddressDetail>(loanApplicationDetails.applicant);

            try { 
                _dbContext.Applicants.Update(applicantToSave);
                _dbContext.Businesses.Update(businessToSave);
                _dbContext.Loans.Update(loanToSave);
                _dbContext.AddressDetails.Update(addrToSave);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in UpdateLoanApplication - Reporsitory Method: {ex}");
            }

            return applicantToSave;

        }
        public Applicant DeleteApplicant(int ApplicantId)
        {

            var applicantRecord = _dbContext.Applicants.Find(ApplicantId);

            try
            {
                if (applicantRecord != null)
                {
                    _dbContext.Remove(applicantRecord);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in DeleteApplicant - Reporsitory Method: {ex}");
            }

            return applicantRecord;
        }

        public IEnumerable<Contract.LoanApplicationDetails> GetLatestApplicants(int size = 10)
        {

            var applicantRecord = (from p in _dbContext.Applicants
                                   join l in _dbContext.Loans
                                   on p.ApplicantId equals l.ApplicantId
                                   join b in _dbContext.Businesses
                                   on p.ApplicantId equals b.ApplicantId
                                   select new Contract.LoanApplicationDetails
                                   {
                                       applicant = new Contract.Applicant
                                       {
                                           ApplicantId = p.ApplicantId,
                                           FirstName = p.FirstName,
                                           MiddleName = p.MiddleName,
                                           LastName = p.LastName,
                                           Email = p.Email
                                          
                                       },
                                       loan = new Contract.Loan
                                       {
                                           LoanId = l.LoanId,
                                           AmountRequested = l.AmountRequested,
                                           Apr = l.Apr,
                                           DateApplied = l.DateApplied
                                       },
                                       business = new Contract.Business
                                       {
                                           BusinessId = b.BusinessId,
                                           Title = b.Title,
                                           Phone = b.Phone
                                       }
                                   }).OrderByDescending(x => x.loan.DateApplied).Take(size).ToList();



            return (IEnumerable<Contract.LoanApplicationDetails>)applicantRecord;
        }

       public  Contract.LoanApplicationDetails GetApplicantDetails(int ApplicantId)
        {

            var applicantRecord = (from apl in _dbContext.Applicants
                                   join ln in _dbContext.Loans
                                   on apl.ApplicantId equals ln.ApplicantId
                                   join b in _dbContext.Businesses
                                   on apl.ApplicantId equals b.ApplicantId
                                   join adr in _dbContext.AddressDetails
                                   on apl.ApplicantId equals adr.ApplicantId
                                   where apl.ApplicantId == ApplicantId
                                   select new Contract.LoanApplicationDetails
                                   {
                                       applicant = new Contract.Applicant
                                       {
                                           ApplicantId = apl.ApplicantId,
                                           FirstName = apl.FirstName,
                                           MiddleName = apl.MiddleName,
                                           LastName = apl.LastName,
                                           Email = apl.Email,
                                           Nationality = apl.Nationality,
                                           Gender = apl.Gender,
                                           DateofBirth = apl.DateofBirth,
                                           Phone = apl.Phone,
                                           AddressId = adr.AddressId,
                                           AddressLine = adr.AddressLine,
                                           City = adr.City,
                                           Country = adr.Country,
                                           State = adr.State,
                                           ZipCode = adr.ZipCode
                                       },
                                      loan = new Contract.Loan
                                       {
                                           LoanId = ln.LoanId,
                                           AmountRequested = ln.AmountRequested,
                                           Apr = ln.Apr,
                                           DateApplied = ln.DateApplied,
                                           CreditRating = ln.CreditRating,
                                           LatePaymentsin5years = ln.LatePaymentsin5years,
                                           NoOfMonthsToPayback = ln.NoOfMonthsToPayback,
                                           NoOfYearsToPayback = ln.NoOfYearsToPayback,
                                           TotalOutstandingDebt = ln.TotalOutstandingDebt,
                                           NoOfOutstaningDebts = ln.NoOfOutstaningDebts,
                                           RiskRating = ln.RiskRating

                                       },
                                       business = new Contract.Business
                                       {
                                           BusinessId = b.BusinessId,
                                           Title = b.Title,
                                           Phone = b.Phone,
                                           Email = b.Email,
                                           WebsiteUrl = b.WebsiteUrl

                                       }
                                   }).ToList().First();

            return (Contract.LoanApplicationDetails)applicantRecord;
        }

       public IEnumerable<Contract.LoanApplicationDetails> SearchApplicants(Contract.LoanApplicationSearch searchParams)
       {



            var applicantRecord = (from p in _dbContext.Applicants
                                   join e in _dbContext.Loans
                                   on p.ApplicantId equals e.ApplicantId
                                   join b in _dbContext.Businesses
                                   on p.ApplicantId equals b.ApplicantId
                                   where p.FirstName == searchParams.ApplicantName
                                   || p.LastName == searchParams.ApplicantName.Trim()
                                   || e.AmountRequested == searchParams.AmountRequested
                                   || e.CreditRating == searchParams.CreditRating
                                   || b.Title == searchParams.BusinessName.Trim()
                                   select new Contract.LoanApplicationDetails
                                   {
                                       applicant = new Contract.Applicant
                                       {
                                           ApplicantId = p.ApplicantId,
                                           FirstName = p.FirstName,
                                           MiddleName = p.MiddleName,
                                           LastName = p.LastName,
                                           Email = p.Email
                                       },
                                       loan = new Contract.Loan
                                       {
                                           LoanId = e.LoanId,
                                           AmountRequested = e.AmountRequested,
                                           Apr= e.Apr,
                                           CreditRating = e.CreditRating,
                                           DateApplied = e.DateApplied
                                       },
                                       business = new Contract.Business
                                       {
                                           BusinessId = b.BusinessId,
                                           Title = b.Title,
                                           Phone = b.Phone
                                       }
                                   }).ToList();

            return (IEnumerable<Contract.LoanApplicationDetails>)applicantRecord;
        }

     
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract = LoanApplicationContracts;
namespace LoanApplicationService.Models
{
    public interface IApplicantRepository
    {
            IEnumerable<Applicant> GetAllApplicants();

           Applicant CreateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails);

            Applicant UpdateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails);

            Applicant DeleteApplicant(int ApplicantId);

            public Contract.LoanApplicationDetails GetApplicantDetails(int ApplicantId);

            IEnumerable<Contract.LoanApplicationDetails> GetLatestApplicants(int size = 10);

            IEnumerable<Contract.LoanApplicationDetails> SearchApplicants(Contract.LoanApplicationSearch searchParams);
        
        }
}

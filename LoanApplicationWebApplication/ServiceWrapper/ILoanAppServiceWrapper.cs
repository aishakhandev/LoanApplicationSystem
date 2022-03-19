using LoanApplicationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApplicationWebApplication.ServiceWrapper
{
   public  interface ILoanAppServiceWrapper
    {
        public IEnumerable<Applicant> GetApplicantsData();

        LoanApplicationDetails GetApplicantDetails(int applicantId);

       string CreateLoanApplication(LoanApplicationDetails applicationDetails);

        IEnumerable<LoanApplicationDetails> SearchApplicantsData(LoanApplicationSearch searchParams);

        IEnumerable<LoanApplicationDetails> GetLatestApplicants(int size);


        string UpdateLoanApplication(LoanApplicationDetails applicationDetails);


        string DeleteApplicant(int applicantId);

    




    }
}

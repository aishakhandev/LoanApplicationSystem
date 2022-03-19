using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoanApplicationContracts
{

    [Serializable]
   public class LoanApplicationDetails
    {
        public Applicant applicant { get; set; }
        public Business business { get; set; }
        public Loan loan { get; set; }     
    }
}

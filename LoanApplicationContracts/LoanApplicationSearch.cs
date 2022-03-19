using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplicationContracts
{
    [Serializable]
   public class LoanApplicationSearch
    {
        public string ApplicantName { get; set; }
        public decimal? AmountRequested { get; set; }
        public DateTime? DateApplied { get; set; }
        public string BusinessName { get; set; }
        public int CreditRating{ get; set; }
    }
}

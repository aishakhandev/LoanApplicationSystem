using System;
using System.Collections.Generic;

#nullable disable

namespace LoanApplicationService.Models
{
    public partial class Loan
    {
        public long LoanId { get; set; }
        public decimal? AmountRequested { get; set; }
        public short? NoOfYearsToPayback { get; set; }
        public short? NoOfMonthsToPayback { get; set; }
        public byte? Apr { get; set; }
        public short? CreditRating { get; set; }
        public short? LatePaymentsin5years { get; set; }
        public int? TotalOutstandingDebt { get; set; }
        public int? RiskRating { get; set; }
        public int? ApplicantId { get; set; }
        public int? BusinessId { get; set; }
        public DateTime? DateApplied { get; set; }
        public int? NoOfOutstaningDebts { get; set; }

        public virtual Applicant Applicant { get; set; }
    }
}

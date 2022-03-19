using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LoanApplicationContracts
{
    [Serializable]
    public class Loan
    {
        public long LoanId { get; set; }
        [Required(ErrorMessage = "Enter the Loan Amount you are applying For.")]
        [Display(Name = "Requested Amount")]
        public decimal? AmountRequested { get; set; }
        [Required]
        [Display(Name = "No. of Years to Payback")]
        public short? NoOfYearsToPayback { get; set; }
        [Required]
        [Display(Name = "No. of Months to Payback")]
        public short? NoOfMonthsToPayback { get; set; }
     
       // [Range(4, 12, ErrorMessage = "Enter number between 4 to 12.")]
        [Display(Name = "APR Rate")]
        public byte? Apr { get; set; }
        [Display(Name = "Credit Rating")]
        public short? CreditRating { get; set; }
        [Display(Name = "Late Payments in 5 years")]
        public short? LatePaymentsin5years { get; set; }
        [Display(Name = "Total Outstanding Debt")]
        public int? TotalOutstandingDebt { get; set; }
        [Display(Name = "Risk Rating")]
        public int? RiskRating { get; set; }
        //public int? ApplicantId { get; set; }
        //public int? BusinessId { get; set; }

        [Display(Name = "Loan Applied Date")]
        public DateTime? DateApplied { get; set; }
        [Display(Name = "No of Outstaning Debts")]
        public int? NoOfOutstaningDebts { get; set; }

        //public Applicant Applicant { get; set; }
        //public Business Business { get; set; }
    }
}

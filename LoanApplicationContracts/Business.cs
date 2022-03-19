using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace LoanApplicationContracts
{
    [Serializable]
    public class Business
    {
        [Display( AutoGenerateField =false)]
        public int BusinessId { get; set; }

        [Display(Name ="Business Title")]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
     
        [Url]
        [Display(Name = "Company Website URL")]
        public string WebsiteUrl { get; set; }

            
        //public int? ApplicantId { get; set; }
       // public Applicant Applicant { get; set; }
       // public BusinessType BusinessType { get; set; }
      //  public List<AddressDetail> AddressDetails { get; set; }
      //  public List<Loan> Loans { get; set; }
    }
}

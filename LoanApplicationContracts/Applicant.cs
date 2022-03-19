using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//#nullable disable

namespace LoanApplicationContracts
{

    // ViewModel for Applicant Entity
    [Serializable]
    public class Applicant
    {
        // Applicant Id
        public int ApplicantId { get; set; }

        // Applicant's First Name - Marked Required
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // Applicant's Last Name - Marked Required
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Applicant's Middle Name- Optional
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        // Applicant's Email Address
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Applicant's Phone Number
        [Required]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DateofBirth { get; set; }

        public string Nationality { get; set; }

        [RegularExpression("[FM]", ErrorMessage = "Enter F or M in capital ")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string AddressLine { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        public int AddressId { get; set; }

        ////  public List<AddressDetail> AddressDetails { get; set; }
        // public AddressDetail AddressDetails { get; set; }
        //public List<Business> Businesses { get; set; }
        //public List<Loan> Loans { get; set; }
    }

    // Enum for labeling each step in Loan Application Creation
    public enum CreateActions
    { 
        CreateApplicant,
        CreateBusiness,
        CreateLoan
    }
}

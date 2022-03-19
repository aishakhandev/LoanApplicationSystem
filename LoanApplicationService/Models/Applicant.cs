using System;
using System.Collections.Generic;

#nullable disable

namespace LoanApplicationService.Models
{
    public partial class Applicant
    {
        public Applicant()
        {
            AddressDetails = new HashSet<AddressDetail>();
            Businesses = new HashSet<Business>();
            Loans = new HashSet<Loan>();
        }

        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}

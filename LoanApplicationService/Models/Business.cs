using System;
using System.Collections.Generic;

#nullable disable

namespace LoanApplicationService.Models
{
    public partial class Business
    {
        public Business()
        {
            AddressDetails = new HashSet<AddressDetail>();
        }

        public int BusinessId { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public int? ApplicantId { get; set; }

        public virtual Applicant Applicant { get; set; }
        public virtual ICollection<AddressDetail> AddressDetails { get; set; }
    }
}

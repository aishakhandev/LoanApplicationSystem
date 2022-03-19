using System;
using System.Collections.Generic;

#nullable disable

namespace LoanApplicationContracts
{
    [Serializable]
    public  class AddressDetail
    {
       
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int? ApplicantId { get; set; }
        public int? BusinessId { get; set; }

    }
}

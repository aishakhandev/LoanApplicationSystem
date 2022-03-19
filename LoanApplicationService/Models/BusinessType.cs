using System;
using System.Collections.Generic;

#nullable disable

namespace LoanApplicationService.Models
{
    public partial class BusinessType
    {
        public BusinessType()
        {
            Businesses = new HashSet<Business>();
        }

        public int BusinessTypeId { get; set; }
        public string BusinessTypeName { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
    }
}

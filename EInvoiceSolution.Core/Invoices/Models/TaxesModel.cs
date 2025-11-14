using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceSolution.Core.Invoices.Models
{
    public class TaxesModel
    {
        public string TaxesType { get; set; }
        public decimal TaxBase { get; set; }
        public decimal Value { get; set; }

    }
}

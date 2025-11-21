using EInvoiceSolution.Core.Invoices.Models;

namespace EInvoice.Api.Common
{
    public static class AccessKeyUtil
    {
        public static string Create(InvoiceModel invoiceModel)
        {
            var emissionDate = invoiceModel.IssueDate.ToString("ddMMyyyy");
            var ruc = invoiceModel.Ruc.PadLeft(13, '0');
            var environment = invoiceModel.Enviroment;
            var series = invoiceModel.Establishment.PadLeft(3, '0') + invoiceModel.EmissionPoint.PadLeft(3, '0');
            var sequential = invoiceModel.Sequential.PadLeft(9, '0');
            var numericCode = DateTime.Now.ToString("yyyyMMdd");
            var emissionType = invoiceModel.EmissionType;
            var documentType = invoiceModel.DocumentCode.PadLeft(2, '0');

            var partialKey = emissionDate + documentType + ruc + environment + series + sequential + numericCode + emissionType;
            var checkDigit = CalculateCheckDigit(partialKey);
            return partialKey + checkDigit;
        }

        private static string CalculateCheckDigit(string key)
        {
            int[] weights = { 2, 3, 4, 5, 6, 7 };
            int weightIndex = 0;
            int sum = 0;

            for (int i = key.Length - 1; i >= 0; i--)
            {
                int digit = key[i] - '0';
                sum += digit * weights[weightIndex];

                weightIndex = (weightIndex + 1) % weights.Length;
            }

            int mod = sum % 11;
            int checkDigit = 11 - mod;

            if (checkDigit == 11) checkDigit = 0;
            else if (checkDigit == 10) checkDigit = 1;

            return checkDigit.ToString();
        }

    }
}

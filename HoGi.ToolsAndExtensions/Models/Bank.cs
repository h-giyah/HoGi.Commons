using System.Collections.Generic;

namespace HoGi.Commons.ToolsAndExtensions.Models
{
    public class Bank
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int SahraCode { get; set; }

        public int RayanCode { get; set; }
        public int TadbirCode { get; set; }

        public int SejamCode { get; set; }
        public string FinnotechCode { get; set; }

        public bool IsActiveForBankAccount { get; set; }
        public IList<string> CardNumbers { get; set; }
    }
}

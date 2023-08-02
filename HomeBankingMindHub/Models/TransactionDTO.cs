using System;
using HomeBankingMindHub.Models;

using System.Collections.Generic;

using System.Text.Json.Serialization;

namespace HomeBankingMindHub.Models
{
    public class TransactionDTO
    {
        
        public long Id { get; set; }

        public string Type { get; set; }
        public double Amount { get; set; }

        public String description { get; set; }

        public DateTime Date { get; set; }

        public Account Account { get; set; }

    }
}

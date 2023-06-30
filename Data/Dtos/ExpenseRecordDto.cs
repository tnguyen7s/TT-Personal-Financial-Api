using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Financial_WebApi.Data.Dtos
{
    public class ExpenseRecordDto
    {
        public string? Category { get; set; }

        public int Spend { get; set; }

        public int Limit { get; set; }
    }
}
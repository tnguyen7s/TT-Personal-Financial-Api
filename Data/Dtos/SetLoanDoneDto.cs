using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Financial_WebApi.Data.Dtos
{
    public class SetLoanDoneDto
    {
        public string? SecondStakeholder { get; set; }

        public DateTime Date { get; set; }

        public int MonthPaid { get; set; }

        public int YearPaid { get; set; }
    }
}
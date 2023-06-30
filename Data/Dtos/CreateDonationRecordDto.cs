using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Financial_WebApi.Data.Dtos
{
    public class CreateDonationRecordDto
    {
        public string ? SentTo {get;set;}

        public DateTime Date{get;set;}

        public int Amount {get;set;}

        public string ? Comment {get;set;}

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
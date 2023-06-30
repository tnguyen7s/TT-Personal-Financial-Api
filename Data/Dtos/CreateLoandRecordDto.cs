using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Financial_WebApi.Data.Dtos
{
    public class CreateLoandRecordDto
    {
        public string ? SecondStakeHolder {get;set;}

        public DateTime Date {get;set;}

        public int Amount {get;set;}

        public bool IsOwner {get;set;}
    }
}
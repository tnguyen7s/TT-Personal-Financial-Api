

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Personal_Financial_WebApi.Data.Entities
{
    [PrimaryKey(nameof(UserIdentifier),nameof(SecondStakeHolder),nameof(Date))]
    
    public class Loan
    {
        [Required]
        public string ? UserIdentifier {get;set;}

        [Required]
        public string ? SecondStakeHolder {get;set;}

        public DateTime Date {get;set;}

        public int Amount {get;set;}

        public bool IsOwner {get;set;}
        
        public bool Done {get;set;}
    }
}
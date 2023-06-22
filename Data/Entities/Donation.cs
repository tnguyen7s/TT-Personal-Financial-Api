
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Personal_Financial_WebApi.Data.Entities
{
    [PrimaryKey(nameof(UserIdentifier),nameof(SentTo),nameof(Date))]
    public class Donation
    {
        [Required]
        public string ? UserIdentifier {get;set;}

        [Required]
        public string ? SentTo {get;set;}

        public DateTime Date{get;set;}

        public int Amount {get;set;}

        public string ? Comment {get;set;}

    }
}


using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Personal_Financial_WebApi.Data.Entities
{
    [PrimaryKey(nameof(UserIdentifier),nameof(Item))]
    public class WishItem
    {
        [Required]
        public string? UserIdentifier {get;set;}

        [Required]
        public string? Item {get;set;} 

        public int Amount {get;set;}

        public string? Comment {get;set;} 
    }
}
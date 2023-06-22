using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Personal_Financial_WebApi.Data.Entities
{
    [PrimaryKey(nameof(Identifier))]
    public class User
    {
        [Required]
        public string? Identifier { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public int TotalBalance { get; set; }

        [Required]
        public int TotalSavingForGood { get; set; }

        [Required]
        public int TotalDonated { get; set; }
    }
}
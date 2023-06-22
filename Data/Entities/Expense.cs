using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Personal_Financial_WebApi.Data.Entities
{
    [PrimaryKey(nameof(UserIdentifier), nameof(Category), nameof(Month), nameof(Year))]
    public class Expense
    {
        [Required]
        public string? UserIdentifier { get; set; }

        [Required]
        public string? Category { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Spend { get; set; }

        public int Limit { get; set; }
    }
}
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

        [Required]
        public DateTime LastCheckin { get; set; }

        public User Clone(){
            return new User{
                Identifier = this.Identifier,
                FullName = this.FullName,
                TotalBalance = this.TotalBalance,
                TotalDonated = this.TotalDonated,
                TotalSavingForGood = this.TotalSavingForGood,
                LastCheckin = this.LastCheckin
            };
        }
    }
}
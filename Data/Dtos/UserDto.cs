
namespace Personal_Financial_WebApi.Data.Dtos
{
    public class UserDto
    {
        public string? Identifier { get; set; }

        public string? FullName { get; set; }

        public DateTime LastCheckin { get; set; }
    }
}
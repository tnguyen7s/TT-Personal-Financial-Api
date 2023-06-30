namespace Personal_Financial_WebApi.Data.Dtos
{
    public class GetStatisticsDataDto
    {
        public string? Category { get; set; }

        public ICollection<SpendDto> Spends { get; set; }
    }
}
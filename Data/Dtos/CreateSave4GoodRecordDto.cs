namespace Personal_Financial_WebApi.Data.Dtos
{
    public class CreateSave4GoodRecordDto
    {
        public string? Item { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }
    }
}
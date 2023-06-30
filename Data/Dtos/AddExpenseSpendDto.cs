namespace Personal_Financial_WebApi.Data.Dtos
{
    public class AddExpenseSpendDto
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string? Category { get; set; }

        public int Amount { get; set; }
    }
}
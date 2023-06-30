namespace Personal_Financial_WebApi.Data.Dtos
{
    public class MonthExpenseRecordDto
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public List<ExpenseRecordDto>? Expenses { get; set; }
    }
}
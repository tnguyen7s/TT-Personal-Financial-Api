namespace Personal_Financial_WebApi.Data.Dtos
{
    public class CreateWishItemRecordDto
    {
        public string? Item {get;set;} 

        public int Amount {get;set;}

        public string? Comment {get;set;} 
    }
}
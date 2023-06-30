// using EntityFramework.Exceptions.Common;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Personal_Financial_WebApi.Data;
// using Personal_Financial_WebApi.Data.Dtos;
// using Personal_Financial_WebApi.Data.Entities;

// namespace Personal_Financial_WebApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UsersController : ControllerBase
//     {
//         private readonly TtDbContext dbContext;

//         public UsersController(TtDbContext dbContext){
//             this.dbContext = dbContext;
//         }

//         [HttpPost("")]
//         public ActionResult<User> CreateUser(UserDto userDto){
//             var newUser = new User();
//             newUser.Identifier = userDto.Identifier;
//             newUser.FullName = userDto.FullName;

//             try{
//                 this.dbContext.Add(newUser);
//                 this.dbContext.SaveChanges();
//             }
//             catch(UniqueConstraintException ex){
//                 return BadRequest("The identifier has been used.");
//             }

//             return Ok(newUser);
//         }

//         [HttpGet("{userIdentifier}")]
//         public ActionResult Checkin(string userIdentifier){
//             var record = this.dbContext
//                         .Users
//                         .Find(userIdentifier);
            
//             var returnedRecord = record.Clone();

//             // update last checkin
//             record.LastCheckin = DateTime.Today;
//             this.dbContext.SaveChanges();

//             return Ok(returnedRecord);
//         }

//         [HttpGet("{userIdentifier}/balance")]
//         public int GetBalance(string userIdentifier){
//             var record = this.dbContext
//                         .Users
//                         .AsNoTracking()
//                         .Where(u=>u.Identifier==userIdentifier).FirstOrDefault();
//             return record==null? -1:record.TotalBalance;
//         }

//         [HttpPatch("{userIdentifier}/balance")]
//         public ActionResult UpdateBalance(string userIdentifier, UpdateBalanceDto dto){
//             var record = this.dbContext
//                         .Users
//                         .Find(userIdentifier);
//             if (record==null){
//                 return BadRequest("No record found");
//             }   

//             // update
//             record.TotalBalance = dto.NewTotalBalance;

//             // save changes
//             this.dbContext.SaveChanges();

//             return NoContent();       
//         }

//         [HttpPost("{userIdentifier}/expenses/{month}/{year}")]
//         public ActionResult CreateNewMonthExpenseRecords(string userIdentifier, int month, int year, List<NewMonthExpenseRecordDto> dtos){
//             var newRecords = new List<Expense>();
//             foreach (var dto in dtos){
//                 newRecords.Add(new Expense{
//                     UserIdentifier = userIdentifier,
//                     Category = dto.Category,
//                     Month = month,
//                     Year = year,
//                     Limit = dto.Limit
//                 });
//             }

//             this.dbContext.AddRange(newRecords);
//             this.dbContext.SaveChanges();
//             return Ok(newRecords);
//         }

//         [HttpGet("{userIdentifier}/expenses/{month}/{year}")]
//         public ActionResult GetExpenseRecords(string userIdentifier, int month, int year){
//             var records = this.dbContext.Expenses.AsNoTracking()
//                                 .Where(e=>e.UserIdentifier==userIdentifier&&e.Month==month&&e.Year==year)
//                                 .Select(e=>new ExpenseRecordDto{
//                                     Category = e.Category,
//                                     Spend = e.Spend,
//                                     Limit = e.Limit
//                                 })
//                                 .ToList();

//             return Ok(records);
//         }

//         [HttpPatch("{userIdentifier}/expenses")]
//         public ActionResult AddExpenseSpend(string userIdentifier, AddExpenseSpendDto dto){
//             var compositeKey = new object[] {userIdentifier, dto.Category, dto.Month, dto.Year};
//             var record = this.dbContext.Expenses.Find(compositeKey);
//             if (record==null){
//                 return BadRequest("No record found");
//             }

//             // update
//             record.Spend = record.Spend + dto.Amount;

//             var userRecord = this.dbContext.Users.Find(userIdentifier);
//             userRecord.TotalBalance = userRecord.TotalBalance - dto.Amount;

//             this.dbContext.SaveChanges();

//             return NoContent();
//         }

//         [HttpPost("{userIdentifier}/saves")]
//         public ActionResult CreateSave4GoodRecord(string userIdentifier, CreateSave4GoodRecordDto dto){
//             // add record
//             var newRecord = new Save4Good{
//                 UserIdentifier = userIdentifier,
//                 Date = dto.Date,
//                 Item = dto.Item,
//                 Amount = dto.Amount
//             };

//             this.dbContext.Add(newRecord);

//             // update the record
//             var userRecord = this.dbContext.Users.Find(userIdentifier);
//             userRecord.TotalSavingForGood = dto.Amount + userRecord.TotalSavingForGood;

//             this.dbContext.SaveChanges();

//             return NoContent();
//         }

//         [HttpGet("{userIdentifier}/saves/")]
//         public ActionResult GetSave4GoodRecords(string userIdentifier, [FromQuery] int pageNumber, [FromQuery] int pageLength){
//             var records = this.dbContext.Save4Goods.AsNoTracking()
//                 .Where(s=>s.UserIdentifier==userIdentifier)
//                 .OrderByDescending(s=>s.Date)
//                 .Skip(pageNumber*pageLength)
//                 .Take(pageLength)
//                 .ToList();

//             return Ok(records);
//         }

//         [HttpPost("{userIdentifier}/donations")]
//         public ActionResult CreateDonationRecord(string userIdentifier, CreateDonationRecordDto dto){
//             // add record
//             var newRecord = new Donation{
//                 UserIdentifier = userIdentifier,
//                 Date = dto.Date,
//                 SentTo = dto.SentTo,
//                 Amount = dto.Amount,
//                 Comment = dto.Comment
//             };

//             this.dbContext.Add(newRecord);

//             // update the user record
//             var userRecord = this.dbContext.Users.Find(userIdentifier);
//             userRecord.TotalDonated = dto.Amount + userRecord.TotalDonated;
//             userRecord.TotalBalance = userRecord.TotalBalance - dto.Amount;


//             // update the expense record
//             var compositeKey = new object[] {userIdentifier, "Donation", dto.Month, dto.Year};
//             var expenseRecord = this.dbContext.Expenses.Find(compositeKey);
//             expenseRecord.Spend = expenseRecord.Spend +dto.Amount;

//             this.dbContext.SaveChanges();

//             return NoContent();
//         }

//         [HttpGet("{userIdentifier}/donations/")]
//         public ActionResult GetDonationRecords(string userIdentifier, [FromQuery] int pageNumber, [FromQuery] int pageLength){
//             var records = this.dbContext.Donations.AsNoTracking()
//                             .Where(s=>s.UserIdentifier==userIdentifier)
//                             .OrderByDescending(s=>s.Date)
//                             .Skip(pageNumber*pageLength)
//                             .Take(pageLength)
//                             .ToList();

//             return Ok(records);
//         }

//        [HttpPost("{userIdentifier}/loans")]
//         public ActionResult CreateLoanRecord(string userIdentifier, CreateLoandRecordDto dto){
//             // add record
//             var newRecord = new Loan{
//                 UserIdentifier = userIdentifier,
//                 Date = dto.Date,
//                 SecondStakeHolder = dto.SecondStakeHolder,
//                 Amount = dto.Amount,
//                 IsOwner = dto.IsOwner
//             };

//             this.dbContext.Add(newRecord);

//             // update the user record
//             var userRecord = this.dbContext.Users.Find(userIdentifier);
//             if (dto.IsOwner){
//                 userRecord.TotalBalance = userRecord.TotalBalance - dto.Amount;
//             }
//             else{
//                 userRecord.TotalBalance = userRecord.TotalBalance + dto.Amount;
//             }

//             this.dbContext.SaveChanges();

//             return NoContent();
//         }

//         [HttpGet("{userIdentifier}/loans/")]
//         public ActionResult GetLoanRecords(string userIdentifier){
//             var records = this.dbContext.Loans.AsNoTracking()
//                                 .Where(s=>s.UserIdentifier==userIdentifier && !s.Done)
//                                 .ToList();

//             return Ok(records);
//         }

//         [HttpPatch("{userIdentifier}/loans/")]
//         public ActionResult SetLoanDone(string userIdentifier, SetLoanDoneDto dto){
//             var compositeKey = new object[]{userIdentifier, dto.SecondStakeholder, dto.Date};
//             var loanRecord = this.dbContext.Loans
//                                     .Find(compositeKey);
//             if (loanRecord.Done){
//                 return BadRequest("Loan has been paid/received");
//             }
//             loanRecord.Done = true;
            
//             var userRecord = this.dbContext.Users.Find(userIdentifier);
//             if (loanRecord.IsOwner){
//                 userRecord.TotalBalance = userRecord.TotalBalance + loanRecord.Amount;
//             }
//             else{
//                 userRecord.TotalBalance = userRecord.TotalBalance - loanRecord.Amount;
//                 var compositeKey2 = new object[] {userIdentifier, "Other", dto.MonthPaid, dto.YearPaid};
//                 var expenseRecord = this.dbContext.Expenses.Find(compositeKey2);
//                 expenseRecord.Spend = expenseRecord.Spend + loanRecord.Amount;
//             }

//             this.dbContext.SaveChanges();
//             return NoContent();
//         }

        
//         [HttpPost("{userIdentifier}/wishlist")]
//         public ActionResult CreateWishListRecord(string userIdentifier, CreateWishItemRecordDto dto){
//             // add record
//             var newRecord = new WishItem{
//                 UserIdentifier = userIdentifier,
//                 Item = dto.Item,
//                 Amount = dto.Amount,
//                 Comment = dto.Comment
//             };

//             this.dbContext.Add(newRecord);

//             this.dbContext.SaveChanges();

//             return NoContent();
//         }

//         [HttpGet("{userIdentifier}/wishlist/")]
//         public ActionResult GetWishList(string userIdentifier){
//             var records = this.dbContext.WishItems.AsNoTracking()
//                                 .Where(s=>s.UserIdentifier==userIdentifier)
//                                 .ToList();

//             return Ok(records);
//         }

//         [HttpPatch("{userIdentifier}/wishlist/")]
//         public ActionResult DeleteWishlistItem(string userIdentifier, DeleteWishlistItemDto dto){
//             var compositeKey = new object[] {userIdentifier, dto.Item};
//             var record = this.dbContext.WishItems.Find(compositeKey);
//             this.dbContext.Remove(record);

//             if (dto.Purchased){                
//                 this.AddExpenseSpend(userIdentifier, new AddExpenseSpendDto{
//                     Month = dto.Date.Month,
//                     Year = dto.Date.Year,
//                     Category = "Other",
//                     Amount = record.Amount
//                 });
//             }
//             else{
//                 this.CreateSave4GoodRecord(userIdentifier, new CreateSave4GoodRecordDto{
//                     Item = dto.Item,
//                     Date = dto.Date,
//                     Amount = record.Amount
//                 });
//             }
//             return NoContent();
//         }

//         [HttpGet("{userIdentifier}/months")]
//         public HashSet<MonthDto> GetMonths(string userIdentifier){
//             return this.dbContext.Expenses.AsNoTracking()
//                     .Where(u=>u.UserIdentifier==userIdentifier)
//                     .OrderByDescending(u=>u.Year)
//                     .OrderByDescending(u=>u.Month)
//                     .Select(u=> new MonthDto{
//                         Month = u.Month,
//                         Year = u.Year
//                     })
//                     .ToHashSet(new MonthYearEqualityComparer());
//         }

//         [HttpGet("{userIdentifier}/expenses/statistics/{month}/{year}")]
//         public Dictionary<string, GetStatisticsDataDto> GetStatisticsData(string userIdentifier, int month, int year){
//             return this.dbContext.Expenses.AsNoTracking()
//                     .Where(u=>u.UserIdentifier==userIdentifier && ((u.Year==year && u.Month>=month)||(u.Year==year+1 && u.Month<month)))
//                     .GroupBy(u=>u.Category)
//                     .Select(
//                         g => new GetStatisticsDataDto{
//                             Category = g.Key,
//                             Spends = g.Select(e => new SpendDto{
//                                 Month = e.Month,
//                                 Year = e.Year,
//                                 Spend = e.Spend
//                             }).ToList()
//                         }
//                     ).ToDictionary(r=>r.Category);

//         }
//     }
// }
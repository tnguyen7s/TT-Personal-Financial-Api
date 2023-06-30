using System.Transactions;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Financial_WebApi.Data;
using Personal_Financial_WebApi.Data.Dtos;
using Personal_Financial_WebApi.Data.Entities;

namespace Personal_Financial_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TtDbContext ttDbContext;

        public UsersController(TtDbContext ttDbContext){
            this.ttDbContext = ttDbContext;
        }

        [HttpPost("")]
        public ActionResult<User> CreateUser(CreateUserDto dto){
            // create user entity
            var newUser = new User{
                Identifier = dto.Identifier,
                FullName = dto.FullName
            };

            // add user
            this.ttDbContext.Add(newUser);

            try{
                // save changes (apply create into the db)
                this.ttDbContext.SaveChanges();
            }
            catch(UniqueConstraintException ex){
                return BadRequest("The identifier has been used.");
            }
            catch (MaxLengthExceededException ex){
                return BadRequest("The identifier must be less than 11 characters.");
            }

            return Ok(newUser);
        }
        
        [HttpPatch("{userIdentifier}")]
        public ActionResult Checkin(string userIdentifier){
            // get user object from the database
            var user = this.ttDbContext.Users
                            .Find(userIdentifier); // ef core create a tracking version of the object

            if (user==null){
                return BadRequest("No user with identifier="+userIdentifier+" found.");
            }

            // update checkin date field
            user.LastCheckin = DateTime.Today;

            // save changes
            this.ttDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{userIdentifier}/expenses/{month}/{year}")]
        public ActionResult CreateNewMonthExpenseRecords(string userIdentifier, int month, int year, List<NewMonthExpenseRecordDto> dtos){
            // create instance of expense
            var expenses = new List<Expense>();
            foreach (var dto in dtos){
                expenses.Add(
                    new Expense {
                        UserIdentifier = userIdentifier,
                        Category = dto.Category,
                        Month = month, 
                        Year = year,
                        Spend = 0,
                        Limit = dto.Limit
                    }
                );
            }

            // add new entities to the database
            this.ttDbContext.AddRange(expenses);

            // call save changes
            this.ttDbContext.SaveChanges();

            return Ok(expenses);
        }

        [HttpGet("{userIdentifier}/expenses/{month}/{year}")]
        public ActionResult GetExpensesByMonth(string userIdentifier, int month, int year){
            var expensesToReturn =  this.ttDbContext.Expenses.AsNoTracking()
                .Where(e=>e.UserIdentifier==userIdentifier&&e.Month==month&&e.Year==year)
                .Select(e=>new {
                    Category = e.Category,
                    Limit = e.Limit,
                    Spend = e.Spend
                }).ToList();

            return Ok(expensesToReturn);
        }

        [HttpPatch("{userIdentifier}/expenses")]
        public ActionResult UpdateSpend(string userIdentifier, AddExpenseSpendDto dto){
            // 1. update expense record
            var compositeKey = new object[]{userIdentifier, dto.Category, dto.Month, dto.Year};
            var record = this.ttDbContext.Expenses
                            .Find(compositeKey);

            if (record==null){
                return BadRequest("No expense record found");
            }
            record.Spend += dto.Amount;

            // 2. update user record
            var userRecord = this.ttDbContext.Users
                                .Find(userIdentifier);
            userRecord.TotalBalance -= dto.Amount;

            using(var transaction= this.ttDbContext.Database.BeginTransaction()){
                // 3. call savechanges
                this.ttDbContext.SaveChanges();
                transaction.Commit();
            }

            return NoContent();
        }
    }


}
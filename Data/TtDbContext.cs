using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Personal_Financial_WebApi.Data.Entities;

namespace Personal_Financial_WebApi.Data
{
    public class TtDbContext:DbContext
    {
        public TtDbContext(DbContextOptions<TtDbContext> options):base(options){

        }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>().Property(u=>u.Identifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<User>().HasIndex(u=>u.Identifier);
            modelBuilder.Entity<User>().Property(u=>u.FullName)
                        .HasColumnType("varchar(50)");
            modelBuilder.Entity<User>().Property(u=>u.TotalBalance)
                        .HasColumnType("money");
            modelBuilder.Entity<User>().Property(u=>u.TotalSavingForGood)
                        .HasColumnType("money");
            modelBuilder.Entity<User>().Property(u=>u.TotalDonated)
                        .HasColumnType("money");


            // Expenses
            modelBuilder.Entity<Expense>().Property(u=>u.UserIdentifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<Expense>().Property(u=>u.Category)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<Expense>().Property(u=>u.Month)
                        .HasColumnType("tinyint");
            modelBuilder.Entity<Expense>().Property(u=>u.Year)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Expense>().Property(u=>u.Spend)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Expense>().Property(u=>u.Limit)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Expense>().HasIndex(e=>new {e.UserIdentifier, e.Category, e.Month, e.Year});
            modelBuilder.Entity<Expense>().HasIndex(e=>new {e.UserIdentifier, e.Month, e.Year});
            modelBuilder.Entity<Expense>().HasIndex(e=>e.UserIdentifier);

            // User <-> Expenses | One 2 Many
            modelBuilder.Entity<User>()
                        .HasMany<Expense>()
                        .WithOne()
                        .HasForeignKey(e=>e.UserIdentifier);

            // Save4Goods |
            modelBuilder.Entity<Save4Good>().Property(u=>u.UserIdentifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<Save4Good>().Property(u=>u.Item)
                        .HasColumnType("varchar(100)");
            modelBuilder.Entity<Save4Good>().Property(u=>u.Date)
                        .HasColumnType("datetime");
            modelBuilder.Entity<Save4Good>().Property(u=>u.Amount)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Save4Good>()
                        .HasIndex(s=>new {s.UserIdentifier, s.Item, s.Date});
            modelBuilder.Entity<Save4Good>().HasIndex(s=>s.UserIdentifier);
            modelBuilder.Entity<User>()
                        .HasMany<Save4Good>()
                        .WithOne()
                        .HasForeignKey(s=>s.UserIdentifier);
            //Donate
            modelBuilder.Entity<Donation>().Property(u=>u.UserIdentifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<Donation>().Property(u=>u.SentTo)
                        .HasColumnType("varchar(100)");
            modelBuilder.Entity<Donation>().Property(u=>u.Date)
                        .HasColumnType("datetime");
            modelBuilder.Entity<Donation>().Property(u=>u.Amount)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Donation>().Property(u=>u.Comment)
                        .HasColumnType("varchar(200)");
            modelBuilder.Entity<Donation>()
                        .HasIndex(s=>new {s.UserIdentifier, s.Date, s.SentTo});
            modelBuilder.Entity<Donation>().HasIndex(d=>d.UserIdentifier);
            modelBuilder.Entity<Donation>()
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey(s=>s.UserIdentifier);

            //Loans
            modelBuilder.Entity<Loan>().Property(u=>u.UserIdentifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<Loan>().Property(u=>u.SecondStakeHolder)
                        .HasColumnType("varchar(50)");
            modelBuilder.Entity<Loan>().Property(u=>u.Date)
                        .HasColumnType("datetime");
            modelBuilder.Entity<Loan>().Property(u=>u.Amount)
                        .HasColumnType("smallint");
            modelBuilder.Entity<Loan>().Property(u=>u.IsOwner)
                        .HasColumnType("tinyint");
            modelBuilder.Entity<Loan>().Property(u=>u.Done)
                        .HasColumnType("tinyint");
            modelBuilder.Entity<Loan>()
                        .HasIndex(e=> new {e.UserIdentifier,e.SecondStakeHolder,e.Date});
            modelBuilder.Entity<Loan>().HasIndex(l=>l.UserIdentifier);
            modelBuilder.Entity<Loan>()
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey(s=>s.UserIdentifier);
            // Wishlist
            modelBuilder.Entity<WishItem>().Property(u=>u.UserIdentifier)
                        .HasColumnType("varchar(10)");
            modelBuilder.Entity<WishItem>().Property(u=>u.Item)
                        .HasColumnType("varchar(100)");       
            modelBuilder.Entity<WishItem>().Property(u=>u.Amount)
                        .HasColumnType("smallmoney");
            modelBuilder.Entity<WishItem>().Property(u=>u.Comment)
                        .HasColumnType("varchar(200)");
            modelBuilder.Entity<WishItem>()
                        .HasIndex(e=>new {e.UserIdentifier,e.Item});
            modelBuilder.Entity<WishItem>().HasIndex(l=>l.UserIdentifier);
            modelBuilder.Entity<WishItem>()
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey(s=>s.UserIdentifier);
        }

        public DbSet<User> Users { get; set; }  

        public DbSet<Expense> Expenses { get; set; }   

        public DbSet<Save4Good> Save4Goods { get; set; }

        public DbSet<Donation> Donations { get; set; }

        public DbSet<Loan> Loans {get;set;}

        public DbSet<WishItem> WishItems {get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BillManager.Business.Entities;
using BillManager.Data.Mappings;

namespace BillManager.Data
{
    public class MyInitializer : DropCreateDatabaseAlways<BillManagerContext>
    {
        public MyInitializer()
        {
        }
        protected override void Seed(BillManagerContext context)
        {
            //Account a = new Account()
            //{
            //    Email = "e@gmail.com",
            //    FirstName = "e",
            //    LastName = "e",
            //    Password = "e",
            //    UserName = "e"
            //};

            //Bill b1 = new Bill()
            //{
            //    AccountId = 1,
            //    AccountNum = "777777",
            //    BillFrequency = BillFrequency.Monthly,
            //    BillKind = BillKind.Reoccurring,
            //    CommenceDate = DateTime.Today,
            //    DayDueInMonth = 12,
            //    Description = "Electric bill",
            //    IsActive = true,
            //    Name = "PGE",
            //    PhoneNum = "503 277-9483",
            //    AutopayIsEnrolled = false
            //};
            //Bill b2 = new Bill()
            //{
            //    AccountId = 1,
            //    AccountNum = "6666667",
            //    BillFrequency = BillFrequency.Monthly,
            //    BillKind = BillKind.Reoccurring,
            //    CommenceDate = DateTime.Today,
            //    DayDueInMonth = 17,
            //    Description = "Gas bill",
            //    IsActive = true,
            //    Name = "NW Natural Gas",
            //    PhoneNum = "503 257-9383",
            //    AutopayIsEnrolled = true
            //};
            //Bill b3 = new Bill()
            //{
            //    AccountId = 1,
            //    AccountNum = "02-256537",
            //    BillFrequency = BillFrequency.Monthly,
            //    BillKind = BillKind.Reoccurring,
            //    CommenceDate = DateTime.Today,
            //    DayDueInMonth = 31,
            //    Description = "Water bill",
            //    IsActive = true,
            //    Name = "Sunrise Water",
            //    PhoneNum = "503 694-6283",
            //    AutopayIsEnrolled = true,
            //    PayOptions = new List<PayOption>()
            //};

            //Website w = new Website()
            //{
            //    AccountId = 1,
            //    Description = "Chase bill pay",
            //    Name = "Chase",
            //    Password = "e",
            //    UrlString = "www.Chase.com",
            //    UserName = "e"
            //};

            //context.Accounts.Add(a);
            //context.Bills.Add(b1);
            //context.Bills.Add(b2);
            //context.Bills.Add(b3);
            //context.Websites.Add(w);
            base.Seed(context);
        }
    }

    public class BillManagerContext : DbContext
    {
        public BillManagerContext()
            : base("name=BillManagerContext")
        {
            //Database.SetInitializer<BillManagerContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<FavoriteLink> FavoriteLinks { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<PayOption> PayOptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new BillMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new FavoriteLinkMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new PayOptionMap());
            modelBuilder.Configurations.Add(new WebsiteMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}












        //static BillManagerContext()
        //{            
        //    Database.SetInitializer<BillManagerContext>(null);
        //}
       
        //static BillManagerContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseAlways<BillManagerContext>());        
        //}
        //public BillManagerContext()
        //    : base()
        //{
        //    this.Configuration.LazyLoadingEnabled = false;
        //}
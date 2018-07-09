namespace TrashCollector.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrashCollector.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TrashCollector.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrashCollector.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            ApplicationDbContext db = new ApplicationDbContext();
            context.Roles.AddOrUpdate(
                s => s.Name,
                    new IdentityRole { Name = "Customer" },
                    new IdentityRole { Name = "Employee" }
                 );
            context.SaveChanges();

            context.Zipcodes.AddOrUpdate(
                s => s.Zip,
                    new Zipcode { Zip = "53215" },
                    new Zipcode { Zip = "53219" }
                 );
            context.SaveChanges();

            context.TrashDays.AddOrUpdate(
                d => d.Day,
                    new TrashDay { Day = "Monday" },
                    new TrashDay { Day = "Tuesday" },
                    new TrashDay { Day = "Wednesday" },
                    new TrashDay { Day = "Thursday" },
                    new TrashDay { Day = "Friday" },
                    new TrashDay { Day = "Saturday" },
                    new TrashDay { Day = "Sunday" }
                );
            context.SaveChanges();

            context.ExtraDays.AddOrUpdate(
                d => d.extra,
                    new ExtraDay { extra = "Monday" },
                    new ExtraDay { extra = "Tuesday" },
                    new ExtraDay { extra = "Wednesday" },
                    new ExtraDay { extra = "Thursday" },
                    new ExtraDay { extra = "Friday" },
                    new ExtraDay { extra = "Saturday" },
                    new ExtraDay { extra = "Sunday" },
                    new ExtraDay { extra = "none" }
                );
            context.SaveChanges();

            context.Customers.AddOrUpdate(
                c => c.Name,
                    new Customer { Name = "Emily Dreyer", Email = "c@gmail.com", Address = "7128 W Harrison Ave", ZipcodeID = 2, TrashDayID = 2, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Jacob Davies", Email = "d@gmail.com", Address = "2608 S 71st St", ZipcodeID = 2, TrashDayID = 3, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Alex Lenard", Email = "e@gmail.com", Address = "2476 S 15th St", ZipcodeID = 1, TrashDayID = 3, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Jennifer Rex", Email = "f@gmail.com", Address = "2443 S 13th St", ZipcodeID = 1, TrashDayID = 4, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Melody Morris", Email = "g@gmail.com", Address = "2467 S 10th St", ZipcodeID = 1, TrashDayID = 4, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Karolin Tobin", Email = "h@gmail.com", Address = "2508 S 10th St", ZipcodeID = 1, TrashDayID = 5, PickUpStatus = false, ExtraID = 8 },
                    new Customer { Name = "Rebecca Hill", Email = "i@gmail.com", Address = "2436 S 8th St", ZipcodeID = 1, TrashDayID = 5, PickUpStatus = false, ExtraID = 8 }
                );
            context.SaveChanges();
        }
    }
}

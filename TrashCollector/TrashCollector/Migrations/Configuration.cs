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
                    new TrashDay { Day = "Sunday" },
                    new TrashDay { Day = "None" }
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
                    new ExtraDay { extra = "None" }
                );
            context.SaveChanges();
        }
    }
}

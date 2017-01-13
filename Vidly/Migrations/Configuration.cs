namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    //http://stackoverflow.com/questions/13510341/entity-framework-5-migrations-setting-up-an-initial-migration-and-single-seed-o
    internal sealed class Configuration : DbMigrationsConfiguration<Vidly.Models.ApplicationDbContext>
    {
        public readonly bool _pendingMigrations;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //var migrator = new DbMigrator(this);
            //_pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(Vidly.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //if (!_pendingMigrations) return;
        }
    }
}

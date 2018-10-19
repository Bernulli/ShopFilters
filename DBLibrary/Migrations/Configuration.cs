namespace DBLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DBLibrary.EF.Context.ShopFilterEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(DBLibrary.EF.Context.ShopFilterEntity context)
        //{

        //}
    }
}

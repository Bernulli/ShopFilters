namespace DBLibrary.EF.Context
{
    using DBLibrary.EF.Configurations;
    using DBLibrary.EF.Models;
    using System.Data.Entity;

    public class ShopFilterEntity : DbContext
    {
        static ShopFilterEntity()
        {
            Database.SetInitializer(new MyContextInitializer());
        }

        public ShopFilterEntity()
            : base("name=ShopFilterEntity")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CompanyConfig());
            modelBuilder.Configurations.Add(new PhoneConfig());
        }
    }
}
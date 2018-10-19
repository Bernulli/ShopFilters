using DBLibrary.EF.Models;
using System.Data.Entity.ModelConfiguration;

namespace DBLibrary.EF.Configurations
{
    class CompanyConfig : EntityTypeConfiguration<Company>
    {
        public CompanyConfig()
        {
            HasKey(k => k.Id);
            Property(n => n.Name).IsRequired().HasMaxLength(255);

            HasMany(p => p.Phone)
                .WithRequired(p => p.Company);
        }
    }
}

using DBLibrary.EF.Models;
using System.Data.Entity.ModelConfiguration;

namespace DBLibrary.EF.Configurations
{
    class PhoneConfig : EntityTypeConfiguration<Phone>
    {
        public PhoneConfig()
        {
            HasKey(k => k.Id);
            Property(n => n.Name).IsRequired().HasMaxLength(255);
            Property(p => p.Price).IsRequired();
            Property(img => img.ImagePath).IsMaxLength().IsRequired();
        }
    }
}

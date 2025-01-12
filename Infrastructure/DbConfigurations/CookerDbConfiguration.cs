using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfigurations
{
    public class CookerDbConfiguration : IEntityTypeConfiguration<Cooker>
    {
        public void Configure(EntityTypeBuilder<Cooker> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Contact).WithMany().HasForeignKey(x => x.ContactId);
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User", "UserDB");
            builder.Property(p => p.Email).HasMaxLength(200).IsRequired(); 
            builder.Property(p => p.Password).HasMaxLength(128).IsRequired();
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Role).HasMaxLength(128).IsRequired();
            builder.Property(p => p.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(128).IsRequired();
            builder.Property(p => p.ManagerID).IsRequired();
            builder.Property(p => p.Birthday);
            builder.Property(p => p.DocNumber).IsRequired(); 

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.To_Dos;
using ToDo.Domain.Users;

namespace ToDo.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x=>x.Username).IsUnique();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsUnicode(false).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(400);
            builder.HasMany(u => u.ToDos).WithOne(t => t.User).HasForeignKey(t => t.OwnerId);


        }
    }
}

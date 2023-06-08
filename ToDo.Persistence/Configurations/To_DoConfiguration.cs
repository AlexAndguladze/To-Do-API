using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Statuses;
using ToDo.Domain.To_Dos;

namespace ToDo.Persistence.Configurations
{
    internal class To_DoConfiguration : IEntityTypeConfiguration<To_Do>
    {
        public void Configure(EntityTypeBuilder<To_Do> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasMany(t => t.Subtasks).WithOne(s => s.ToDo).HasForeignKey(s => s.ToDoItemId);
            builder.Property(t => t.CompletionDate).IsRequired(false);
            builder.Property(t => t.Status).HasDefaultValue(Status.Active).HasConversion<string>();
        }
    }
}

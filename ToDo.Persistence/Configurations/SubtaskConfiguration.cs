using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Subtasks;

namespace ToDo.Persistence.Configurations
{
    internal class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.HasKey(s=>s.Id);
            builder.Property(s => s.ToDoItemId).IsRequired();
            builder.Property(s=>s.Title).IsRequired().HasMaxLength(100);
        }
    }
}

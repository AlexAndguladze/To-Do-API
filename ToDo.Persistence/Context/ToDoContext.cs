using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Subtasks;
using ToDo.Domain.To_Dos;
using ToDo.Domain.Users;

namespace ToDo.Persistence.Context
{
    public class ToDoContext:DbContext
    {
        
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<To_Do> ToDos { get; set; }
        public DbSet<Subtask> Subtask { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseEntity && (
              e.State == EntityState.Added
              || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).ModifiedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoContext).Assembly);
        }

    }
}

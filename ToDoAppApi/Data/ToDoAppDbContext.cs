using Microsoft.EntityFrameworkCore;

namespace ToDoAppApi.Data
{
    public class ToDoAppDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options)
        : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;database=todoappdb;user=root;password=your_password;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Todos)
                .HasForeignKey(t => t.CategoryId);
        }

    }
}

﻿namespace Suls.Data
{
    using Microsoft.EntityFrameworkCore;

    public class SulsDbContext : DbContext
    {
        public SulsDbContext()
        {
        }

        public SulsDbContext(DbContextOptions db)
            :base(db)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Suls;Integrated Security=true");
            }
        }
    }
}

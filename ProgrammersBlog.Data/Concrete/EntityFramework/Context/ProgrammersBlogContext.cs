using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Concrete.EntityFramework.Mappings;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Context
{
    public class ProgrammersBlogContext :DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ProgrammersBlog;
                                                     Trusted_Connection=true;ConnectTimeout=30;MultipleActiveResultSets=true;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMapper());
            modelBuilder.ApplyConfiguration(new UserMapoer());
            modelBuilder.ApplyConfiguration(new RoleMapper());
            modelBuilder.ApplyConfiguration(new ArticleMapper());
            modelBuilder.ApplyConfiguration(new CommentMapper());
        }
    }
}

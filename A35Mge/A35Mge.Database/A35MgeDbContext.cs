using A35Mge.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database
{
    public class A35MgeDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }


        public A35MgeDbContext(DbContextOptions<A35MgeDbContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                           .HasAnnotation("ProductVersion", "3.1.8")
                           .HasAnnotation("Relational:MaxIdentifierLength", 64);


            //modelBuilder.Entity("Test.Database.Entities.Hobbies", b =>
            //{
            //    b.HasOne("Test.Database.Entities.User", "User")
            //        .WithMany("Hobbies")
            //        .HasForeignKey("UserId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});
        }

    }
}

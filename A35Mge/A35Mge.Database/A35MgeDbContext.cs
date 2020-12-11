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
        public DbSet<LanguageType> LanguageType { get; set; }
        public DbSet<Translate> Translate { get; set; }

        //public DbSet<Client> Client { get; set; }

        public A35MgeDbContext(DbContextOptions<A35MgeDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                           .HasAnnotation("ProductVersion", "3.1.10")
                           .HasAnnotation("Relational:MaxIdentifierLength", 64);
            modelBuilder.Entity("A35Mge.Database.Entities.Translate", b =>
            {
                b.HasOne("A35Mge.Database.Entities.LanguageType", "LanguageType")
                    .WithMany("TranslateList")
                    .HasForeignKey("LanguageTypeId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        }

    }
}

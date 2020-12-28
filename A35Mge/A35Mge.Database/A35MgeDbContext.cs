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
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<LanguageType> LanguageType { get; set; }
        public DbSet<Translate> Translate { get; set; }
        public DbSet<Menu> Menu { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<JobSchedule> JobSchedule { get; set; }

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
            modelBuilder.Entity("A35Mge.Database.Entities.RoleMenu", b =>
            {
                b.HasOne("A35Mge.Database.Entities.Menu", "Menu")
                    .WithMany("roleMenus")
                    .HasForeignKey("MenuId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("A35Mge.Database.Entities.Role", "Role")
                    .WithMany("roleMenus")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }

    }
}

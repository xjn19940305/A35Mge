﻿// <auto-generated />
using System;
using A35Mge.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace A35Mge.MySqlDatabase.Migrations
{
    [DbContext(typeof(A35MgeDbContext))]
    [Migration("20201217055420_changeJobStruct")]
    partial class changeJobStruct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("A35Mge.Database.Entities.JobSchedule", b =>
                {
                    b.Property<int>("JobScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CronExpression")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("JobName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("JobStatu")
                        .HasColumnType("int");

                    b.Property<int?>("LoopType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Params")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("StartNow")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TriggerType")
                        .HasColumnType("int");

                    b.HasKey("JobScheduleId");

                    b.ToTable("JobSchedule");
                });

            modelBuilder.Entity("A35Mge.Database.Entities.LanguageType", b =>
                {
                    b.Property<int>("LanguageTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("LanguageTypeId");

                    b.ToTable("LanguageType");
                });

            modelBuilder.Entity("A35Mge.Database.Entities.Menu", b =>
                {
                    b.Property<string>("MenuId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Component")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsBtn")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ParentId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Path")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Show")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("hideChildren")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("keepAlive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("redirect")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("MenuId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("A35Mge.Database.Entities.Translate", b =>
                {
                    b.Property<int>("TranslateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("LanguageTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TranslateCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TranslateContent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TranslateId");

                    b.HasIndex("LanguageTypeId");

                    b.ToTable("Translate");
                });

            modelBuilder.Entity("A35Mge.Database.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("A35Mge.Database.Entities.Translate", b =>
                {
                    b.HasOne("A35Mge.Database.Entities.LanguageType", "LanguageType")
                        .WithMany("TranslateList")
                        .HasForeignKey("LanguageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

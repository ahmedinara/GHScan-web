﻿// <auto-generated />
using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211019142814_Frist Migratin")]
    partial class FristMigratin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.MobileScannedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormatedQrCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinshed")
                        .HasColumnType("bit");

                    b.Property<string>("QrCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ScannedGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedBy" }, "IX_MobileScannedItem_CreatedBy");

                    b.ToTable("MobileScannedItem");
                });

            modelBuilder.Entity("Core.Entities.ScannedDetial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QrCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QrFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScannedMasterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ScannedMasterId" }, "IX_ScannedDetial_ScannedMasterId");

                    b.ToTable("ScannedDetial");
                });

            modelBuilder.Entity("Core.Entities.ScannedHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountOfItems")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MailRecive")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MailSent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("ScannedGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedBy" }, "IX_ScannedHeader_CreatedBy");

                    b.ToTable("ScannedHeader");
                });

            modelBuilder.Entity("Core.Entities.SettingConfig", b =>
                {
                    b.Property<string>("SettingKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SettingGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettingValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SettingKey");

                    b.ToTable("SettingConfigs");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FristName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNo")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength(true);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varbinary(20)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Core.Entities.MobileScannedItem", b =>
                {
                    b.HasOne("Core.Entities.User", "CreatedByNavigation")
                        .WithMany("MobileScannedItems")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_MobileScannedItem_User")
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("Core.Entities.ScannedDetial", b =>
                {
                    b.HasOne("Core.Entities.ScannedHeader", "ScannedMaster")
                        .WithMany("ScannedDetials")
                        .HasForeignKey("ScannedMasterId")
                        .HasConstraintName("FK_ScannedDetial_ScannedHeader")
                        .IsRequired();

                    b.Navigation("ScannedMaster");
                });

            modelBuilder.Entity("Core.Entities.ScannedHeader", b =>
                {
                    b.HasOne("Core.Entities.User", "CreatedByNavigation")
                        .WithMany("ScannedHeaders")
                        .HasForeignKey("CreatedBy")
                        .HasConstraintName("FK_ScannedHeader_User")
                        .IsRequired();

                    b.Navigation("CreatedByNavigation");
                });

            modelBuilder.Entity("Core.Entities.ScannedHeader", b =>
                {
                    b.Navigation("ScannedDetials");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("MobileScannedItems");

                    b.Navigation("ScannedHeaders");
                });
#pragma warning restore 612, 618
        }
    }
}

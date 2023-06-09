﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lab3.Repository;

#nullable disable

namespace lab3.Migrations
{
    [DbContext(typeof(DatabaseContexts))]
    partial class DatabaseContextsModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("lab3.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("lab3.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Abv")
                        .HasColumnType("real");

                    b.Property<int?>("DrinkTypeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrinkTypeId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("lab3.Models.DrinkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrOfBrands")
                        .HasColumnType("int");

                    b.Property<float>("ProfitMargin")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DrinkTypes");
                });

            modelBuilder.Entity("lab3.Models.Order", b =>
                {
                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NrOfDrinks")
                        .HasColumnType("int");

                    b.HasKey("DrinkId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("lab3.Models.Drink", b =>
                {
                    b.HasOne("lab3.Models.DrinkType", "DrinkType")
                        .WithMany("Drinks")
                        .HasForeignKey("DrinkTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrinkType");
                });

            modelBuilder.Entity("lab3.Models.Order", b =>
                {
                    b.HasOne("lab3.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lab3.Models.Drink", "Drink")
                        .WithMany("Orders")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Drink");
                });

            modelBuilder.Entity("lab3.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("lab3.Models.Drink", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("lab3.Models.DrinkType", b =>
                {
                    b.Navigation("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}

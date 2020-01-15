﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupremeBot.Data;

namespace SupremeBot.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200115213833_sitee")]
    partial class sitee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SupremeBot.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("PostCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SupremeBot.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVV")
                        .IsRequired();

                    b.Property<string>("Month")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<string>("Year")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("SupremeBot.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("SupremeBot.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AnyColor");

                    b.Property<int>("Category");

                    b.Property<int>("Size");

                    b.Property<int?>("TaskItemId");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SupremeBot.Models.ItemName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemNames");
                });

            modelBuilder.Entity("SupremeBot.Models.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("SiteUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SupremeBot.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<bool>("AnyColor");

                    b.Property<int?>("CardId");

                    b.Property<int>("Delay");

                    b.Property<bool>("FillAdress");

                    b.Property<int>("Hour");

                    b.Property<int>("Minute");

                    b.Property<string>("Name");

                    b.Property<bool>("OnlyWithEmptyBasket");

                    b.Property<int>("RefreshInterval");

                    b.Property<int>("Second");

                    b.Property<int?>("SiteId");

                    b.Property<bool>("UseTimer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CardId");

                    b.HasIndex("SiteId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("SupremeBot.Models.Color", b =>
                {
                    b.HasOne("SupremeBot.Models.Item")
                        .WithMany("Colors")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("SupremeBot.Models.Item", b =>
                {
                    b.HasOne("SupremeBot.Models.TaskItem")
                        .WithMany("Items")
                        .HasForeignKey("TaskItemId");
                });

            modelBuilder.Entity("SupremeBot.Models.ItemName", b =>
                {
                    b.HasOne("SupremeBot.Models.Item")
                        .WithMany("Names")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("SupremeBot.Models.TaskItem", b =>
                {
                    b.HasOne("SupremeBot.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("SupremeBot.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("SupremeBot.Models.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");
                });
#pragma warning restore 612, 618
        }
    }
}

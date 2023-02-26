﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aaaSystemsApi;

#nullable disable

namespace aaaSystemsApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230225204943_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("aaaSystemsCommon.Entity.Dialog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ChatId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNeedAnswer")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ChatId")
                        .IsUnique();

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("aaaSystemsCommon.Entity.DialogMessage", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<long>("DialogId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.ToTable("DialogMessages");
                });

            modelBuilder.Entity("aaaSystemsCommon.Entity.Sender", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Senders");
                });

            modelBuilder.Entity("aaaSystemsCommon.Entity.Dialog", b =>
                {
                    b.HasOne("aaaSystemsCommon.Entity.Sender", null)
                        .WithOne()
                        .HasForeignKey("aaaSystemsCommon.Entity.Dialog", "ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aaaSystemsCommon.Entity.DialogMessage", b =>
                {
                    b.HasOne("aaaSystemsCommon.Entity.Dialog", null)
                        .WithMany()
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
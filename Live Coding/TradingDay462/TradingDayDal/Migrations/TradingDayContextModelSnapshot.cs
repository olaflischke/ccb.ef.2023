﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradingDayDal;

namespace TradingDayDal.Migrations
{
    [DbContext(typeof(TradingDayContext))]
    partial class TradingDayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32");

            modelBuilder.Entity("TradingDayDal.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("EuroRate")
                        .HasColumnType("REAL");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TradingDayId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TradingDayId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TradingDayDal.Entities.TradingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TradingDays");
                });

            modelBuilder.Entity("TradingDayDal.Entities.Currency", b =>
                {
                    b.HasOne("TradingDayDal.Entities.TradingDay", "TradingDay")
                        .WithMany("Currencies")
                        .HasForeignKey("TradingDayId");
                });
#pragma warning restore 612, 618
        }
    }
}

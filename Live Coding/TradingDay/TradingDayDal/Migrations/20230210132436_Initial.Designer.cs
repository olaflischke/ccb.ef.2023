// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradingDayDal;

namespace TradingDayDal.Migrations
{
    [DbContext(typeof(TradingDayContext))]
    [Migration("20230210132436_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("TradingDayDal.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("EuroRate")
                        .HasColumnType("REAL");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TradingDayId")
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
                        .HasForeignKey("TradingDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradingDay");
                });

            modelBuilder.Entity("TradingDayDal.Entities.TradingDay", b =>
                {
                    b.Navigation("Currencies");
                });
#pragma warning restore 612, 618
        }
    }
}

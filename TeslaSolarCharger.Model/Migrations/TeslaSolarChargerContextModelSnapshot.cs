﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeslaSolarCharger.Model.EntityFramework;

#nullable disable

namespace TeslaSolarCharger.Model.Migrations
{
    [DbContext(typeof(TeslaSolarChargerContext))]
    partial class TeslaSolarChargerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.CachedCarState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarStateJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CachedCarStates");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.ChargePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("GridPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SolarPrice")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidSince")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ChargePrices");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.HandledCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("CalculatedPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChargePriceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChargingProcessId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("UsedGridEnergy")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("UsedSolarEnergy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChargePriceId");

                    b.ToTable("HandledCharges");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.PowerDistribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChargingPower")
                        .HasColumnType("INTEGER");

                    b.Property<float>("GridProportion")
                        .HasColumnType("REAL");

                    b.Property<int>("HandledChargeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PowerFromGrid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HandledChargeId");

                    b.ToTable("PowerDistributions");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.HandledCharge", b =>
                {
                    b.HasOne("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.ChargePrice", "ChargePrice")
                        .WithMany("HandledCharges")
                        .HasForeignKey("ChargePriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargePrice");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.PowerDistribution", b =>
                {
                    b.HasOne("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.HandledCharge", "HandledCharge")
                        .WithMany("PowerDistributions")
                        .HasForeignKey("HandledChargeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HandledCharge");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.ChargePrice", b =>
                {
                    b.Navigation("HandledCharges");
                });

            modelBuilder.Entity("TeslaSolarCharger.Model.Entities.TeslaSolarCharger.HandledCharge", b =>
                {
                    b.Navigation("PowerDistributions");
                });
#pragma warning restore 612, 618
        }
    }
}

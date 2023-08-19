﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mqtt.Persistence;

#nullable disable

namespace Mqtt.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mqtt.Domain.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Mqtt.Domain.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("SensorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TimestampId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.HasIndex("TimestampId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Mqtt.Domain.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Mqtt.Domain.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UnitId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("Mqtt.Domain.Timestamp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Timestamps");
                });

            modelBuilder.Entity("Mqtt.Domain.Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset?>("Updated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Units");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f5dcc688-e60a-421d-ae44-ac527f7bfba7"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "TEMP",
                            ShortName = "°C"
                        },
                        new
                        {
                            Id = new Guid("7d85889d-b1f2-487b-aaff-8f3e8f6af445"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "HUM",
                            ShortName = "%"
                        },
                        new
                        {
                            Id = new Guid("266e2ee3-869a-473e-8c26-ed333deb9301"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "PRESS",
                            ShortName = "hPa"
                        },
                        new
                        {
                            Id = new Guid("8b9460ea-6c8e-4dd4-bbe0-a51537f9ce5a"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "ETHANOL",
                            ShortName = "%"
                        },
                        new
                        {
                            Id = new Guid("37cd8490-4d8d-41f2-a188-6c5e74c6e223"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "CO2",
                            ShortName = "%"
                        },
                        new
                        {
                            Id = new Guid("a727103f-5342-428b-9d9a-b3a0958b1640"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "TVOC",
                            ShortName = "%"
                        },
                        new
                        {
                            Id = new Guid("dac4dba1-0d62-4ba7-90a7-36666cb0caca"),
                            Created = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Name = "H2",
                            ShortName = "%"
                        });
                });

            modelBuilder.Entity("Mqtt.Domain.Device", b =>
                {
                    b.HasOne("Mqtt.Domain.Room", "Room")
                        .WithMany("Devices")
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Mqtt.Domain.Measurement", b =>
                {
                    b.HasOne("Mqtt.Domain.Sensor", "Sensor")
                        .WithMany("Measurements")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mqtt.Domain.Timestamp", "Timestamp")
                        .WithMany("Measurements")
                        .HasForeignKey("TimestampId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");

                    b.Navigation("Timestamp");
                });

            modelBuilder.Entity("Mqtt.Domain.Sensor", b =>
                {
                    b.HasOne("Mqtt.Domain.Device", "Device")
                        .WithMany("Sensors")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mqtt.Domain.Unit", "Unit")
                        .WithMany("Sensors")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Mqtt.Domain.Device", b =>
                {
                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("Mqtt.Domain.Room", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Mqtt.Domain.Sensor", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("Mqtt.Domain.Timestamp", b =>
                {
                    b.Navigation("Measurements");
                });

            modelBuilder.Entity("Mqtt.Domain.Unit", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}

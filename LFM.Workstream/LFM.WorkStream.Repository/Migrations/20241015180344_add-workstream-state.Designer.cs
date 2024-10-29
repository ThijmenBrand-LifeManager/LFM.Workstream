﻿// <auto-generated />
using System;
using LFM.WorkStream.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LFM.WorkStream.Repository.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241015180344_add-workstream-state")]
    partial class addworkstreamstate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LFM.WorkStream.Core.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2024, 10, 15, 20, 3, 44, 5, DateTimeKind.Unspecified).AddTicks(4570), new TimeSpan(0, 2, 0, 0, 0)));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("WorkStreamId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WorkStreamId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkStreamId1");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("LFM.WorkStream.Core.Models.WorkStream", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2024, 10, 15, 20, 3, 44, 5, DateTimeKind.Unspecified).AddTicks(3930), new TimeSpan(0, 2, 0, 0, 0)));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WorkStreams");
                });

            modelBuilder.Entity("LFM.WorkStream.Core.Models.Project", b =>
                {
                    b.HasOne("LFM.WorkStream.Core.Models.WorkStream", "WorkStream")
                        .WithMany("Projects")
                        .HasForeignKey("WorkStreamId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkStream");
                });

            modelBuilder.Entity("LFM.WorkStream.Core.Models.WorkStream", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
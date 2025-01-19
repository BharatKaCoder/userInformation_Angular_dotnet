﻿// <auto-generated />
using ICC_Champion_Trophy_2025;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ICC_Champion_Trophy_2025.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250119135355_AddPlayerDetailstables--ignore-changes")]
    partial class AddPlayerDetailstablesignorechanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.PlayerDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("PlaterDetails");
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.Players", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HighScore")
                        .HasColumnType("int");

                    b.Property<int>("Matches")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.Teams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Captain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Champions")
                        .HasColumnType("int");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.PlayerDetails", b =>
                {
                    b.HasOne("ICC_Champion_Trophy_2025.Model.Players", "Player")
                        .WithOne("PlayerDetails")
                        .HasForeignKey("ICC_Champion_Trophy_2025.Model.PlayerDetails", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.Players", b =>
                {
                    b.HasOne("ICC_Champion_Trophy_2025.Model.Teams", "Teams")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.Players", b =>
                {
                    b.Navigation("PlayerDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("ICC_Champion_Trophy_2025.Model.Teams", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}

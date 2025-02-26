﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace WEB_projekat.Migrations
{
    [DbContext(typeof(AutoPlacContext))]
    [Migration("20220112133243_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.AutoPlac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Placevi");
                });

            modelBuilder.Entity("Models.Prodavac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BrLicneKarte")
                        .HasMaxLength(9)
                        .HasColumnType("bigint");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Prodavci");
                });

            modelBuilder.Entity("Models.TipKaroserije", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Karoserije");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<int>("GodinaProizvodnje")
                        .HasColumnType("int");

                    b.Property<int?>("KaroserijaID")
                        .HasColumnType("int");

                    b.Property<int>("Kilometraza")
                        .HasColumnType("int");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Model")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("NazivPlacaID")
                        .HasColumnType("int");

                    b.Property<string>("RegistarskaTablica")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("SnagaMotora")
                        .HasColumnType("int");

                    b.Property<int?>("VlasnikID")
                        .HasColumnType("int");

                    b.Property<int>("ZapreminaMotora")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KaroserijaID");

                    b.HasIndex("NazivPlacaID");

                    b.HasIndex("VlasnikID");

                    b.ToTable("Vozilo");
                });

            modelBuilder.Entity("Models.Vozilo", b =>
                {
                    b.HasOne("Models.TipKaroserije", "Karoserija")
                        .WithMany("VozilaSaTipomKaroserije")
                        .HasForeignKey("KaroserijaID");

                    b.HasOne("Models.AutoPlac", "NazivPlaca")
                        .WithMany("Vozila")
                        .HasForeignKey("NazivPlacaID");

                    b.HasOne("Models.Prodavac", "Vlasnik")
                        .WithMany("ListaVozila")
                        .HasForeignKey("VlasnikID");

                    b.Navigation("Karoserija");

                    b.Navigation("NazivPlaca");

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("Models.AutoPlac", b =>
                {
                    b.Navigation("Vozila");
                });

            modelBuilder.Entity("Models.Prodavac", b =>
                {
                    b.Navigation("ListaVozila");
                });

            modelBuilder.Entity("Models.TipKaroserije", b =>
                {
                    b.Navigation("VozilaSaTipomKaroserije");
                });
#pragma warning restore 612, 618
        }
    }
}

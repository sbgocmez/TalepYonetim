﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalepYonetim.Data;

#nullable disable

namespace TalepYonetim.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TalepYonetim.Model.AltKategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("AltKategoriler");
                });

            modelBuilder.Entity("TalepYonetim.Model.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("TalepYonetim.Model.Talep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Adet")
                        .HasColumnType("int");

                    b.Property<int>("AltKategoriId")
                        .HasColumnType("int");

                    b.Property<string>("EdenSoyisim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Edenİsim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AltKategoriId");

                    b.ToTable("Talepler");
                });

            modelBuilder.Entity("TalepYonetim.Model.AltKategori", b =>
                {
                    b.HasOne("TalepYonetim.Model.Kategori", "Kategori")
                        .WithMany("AltKategoriler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("TalepYonetim.Model.Talep", b =>
                {
                    b.HasOne("TalepYonetim.Model.AltKategori", "AltKategori")
                        .WithMany("Talepler")
                        .HasForeignKey("AltKategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AltKategori");
                });

            modelBuilder.Entity("TalepYonetim.Model.AltKategori", b =>
                {
                    b.Navigation("Talepler");
                });

            modelBuilder.Entity("TalepYonetim.Model.Kategori", b =>
                {
                    b.Navigation("AltKategoriler");
                });
#pragma warning restore 612, 618
        }
    }
}

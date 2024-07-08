﻿// <auto-generated />
using System;
using LibrarySystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibrarySystem.Migrations
{
    [DbContext(typeof(BookLendingDb))]
    partial class BookLendingDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("LibrarySystem.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Edition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Isbn")
                        .HasMaxLength(17)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<int>("PageCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibrarySystem.Models.Lending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId", "From", "To")
                        .IsUnique();

                    b.ToTable("Lendings");
                });

            modelBuilder.Entity("LibrarySystem.Models.Lending", b =>
                {
                    b.HasOne("LibrarySystem.Models.Book", "Book")
                        .WithMany("Lendings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibrarySystem.Models.Book", b =>
                {
                    b.Navigation("Lendings");
                });
#pragma warning restore 612, 618
        }
    }
}

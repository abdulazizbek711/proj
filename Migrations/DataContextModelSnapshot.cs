﻿// <auto-generated />
using MarketApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketApi.Models.Order", b =>
                {
                    b.Property<int>("Order_number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Order_number"));

                    b.Property<int>("OrderQuantity_type")
                        .HasColumnType("integer");

                    b.Property<double>("Price_Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Price_Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Product_ID")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<int>("User_ID")
                        .HasColumnType("integer");

                    b.Property<int>("User_ID1")
                        .HasColumnType("integer");

                    b.HasKey("Order_number");

                    b.HasIndex("User_ID1");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MarketApi.Models.Product", b =>
                {
                    b.Property<int>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Product_ID"));

                    b.Property<double>("Price_Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Price_Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductQuantity_type")
                        .HasColumnType("integer");

                    b.Property<string>("Product_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.HasKey("Product_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MarketApi.Models.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("User_ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("User_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<int>("OrdersOrder_number")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsProduct_ID")
                        .HasColumnType("integer");

                    b.HasKey("OrdersOrder_number", "ProductsProduct_ID");

                    b.HasIndex("ProductsProduct_ID");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("MarketApi.Models.Order", b =>
                {
                    b.HasOne("MarketApi.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("User_ID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("MarketApi.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrder_number")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarketApi.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProduct_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarketApi.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
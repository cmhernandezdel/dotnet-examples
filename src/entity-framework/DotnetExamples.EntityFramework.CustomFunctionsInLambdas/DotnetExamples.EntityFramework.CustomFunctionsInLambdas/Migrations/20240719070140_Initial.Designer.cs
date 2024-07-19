﻿// <auto-generated />
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240719070140_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}

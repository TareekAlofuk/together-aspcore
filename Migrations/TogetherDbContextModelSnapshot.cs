﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using together_aspcore.Shared;

namespace together_aspcore.Migrations
{
    [DbContext(typeof(TogetherDbContext))]
    partial class TogetherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("together_aspcore.App.Member.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("Archived");

                    b.Property<DateTime>("BirthDate");

                    b.Property<bool>("Disabled");

                    b.Property<string>("Email");

                    b.Property<string>("FaceImage");

                    b.Property<string>("Name");

                    b.Property<string>("PassportImage");

                    b.Property<string>("Phone");

                    b.Property<string>("Phone2");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });
#pragma warning restore 612, 618
        }
    }
}

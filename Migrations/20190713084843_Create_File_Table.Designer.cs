﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using together_aspcore.Shared;

namespace together_aspcore.Migrations
{
    [DbContext(typeof(TogetherDbContext))]
    [Migration("20190713084843_Create_File_Table")]
    partial class Create_File_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("together_aspcore.App.Member.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int?>("MemberId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("together_aspcore.App.Member.File", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path");

                    b.HasKey("Code");

                    b.ToTable("Files");
                });

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

            modelBuilder.Entity("together_aspcore.App.Member.Credential", b =>
                {
                    b.HasOne("together_aspcore.App.Member.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");
                });
#pragma warning restore 612, 618
        }
    }
}

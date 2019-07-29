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

            modelBuilder.Entity("together_aspcore.App.Member.MemberCredentials", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("MemberId");

                    b.ToTable("MembersCredentials");
                });

            modelBuilder.Entity("together_aspcore.App.Member.MemberFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayFileName");

                    b.Property<string>("FileName");

                    b.Property<int>("MemberId");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("together_aspcore.App.Member.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("Archived");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("Date");

                    b.Property<bool>("Disabled");

                    b.Property<string>("Email");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("Date");

                    b.Property<string>("FaceImage");

                    b.Property<string>("JobTitle");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("PassportExpirationDate")
                        .HasColumnType("Date");

                    b.Property<string>("PassportImage");

                    b.Property<string>("PassportNo")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("SecondaryPhone");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.MembershipServiceDefault", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Count");

                    b.Property<int>("MembershipType");

                    b.Property<int?>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceMembershipDefaults");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.ServiceRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Discount");

                    b.Property<int?>("DiscountOptions");

                    b.Property<int>("LimitType");

                    b.Property<int>("MembershipType");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServicesRules");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.ServiceStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("Date");

                    b.Property<int>("MemberId");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServicesStore");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.ServiceUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Commission");

                    b.Property<double?>("Count");

                    b.Property<double?>("Discount");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("Date");

                    b.Property<double?>("FinalPrice");

                    b.Property<int>("MemberId");

                    b.Property<string>("Notes");

                    b.Property<double?>("Price");

                    b.Property<int>("ReferencePerson");

                    b.Property<int>("ServiceId");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.ToTable("ServicesUsages");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.MembershipServiceDefault", b =>
                {
                    b.HasOne("together_aspcore.App.Service.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.ServiceRule", b =>
                {
                    b.HasOne("together_aspcore.App.Service.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("together_aspcore.App.Service.Models.ServiceStore", b =>
                {
                    b.HasOne("together_aspcore.App.Member.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("together_aspcore.App.Service.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Blog.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog.Infra.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240216165358_ReameColumnPost")]
    partial class ReameColumnPost
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Blog.Domain.AuthenticationContext.AccountAggregate.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("email");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("account", "public");
                });

            modelBuilder.Entity("Blog.Domain.BlogContext.PostAggregate.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)")
                        .HasColumnName("thumbnail");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("WriterId")
                        .HasColumnType("uuid")
                        .HasColumnName("writer_id");

                    b.HasKey("Id");

                    b.HasIndex("WriterId");

                    b.ToTable("post", "public");
                });

            modelBuilder.Entity("Blog.Domain.BlogContext.WriterAggregate.Writer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<string>("CoverLetter")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("cover_letter");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)")
                        .HasColumnName("photo");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("writer", "public");
                });

            modelBuilder.Entity("Blog.Domain.AuthenticationContext.AccountAggregate.Account", b =>
                {
                    b.OwnsOne("Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uuid");

                            b1.Property<byte[]>("Hash")
                                .IsRequired()
                                .HasColumnType("bytea")
                                .HasColumnName("hash");

                            b1.Property<byte[]>("Salt")
                                .IsRequired()
                                .HasColumnType("bytea")
                                .HasColumnName("salt");

                            b1.HasKey("AccountId");

                            b1.ToTable("account", "public");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Domain.BlogContext.PostAggregate.Post", b =>
                {
                    b.HasOne("Blog.Domain.BlogContext.WriterAggregate.Writer", null)
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Domain.BlogContext.WriterAggregate.Writer", b =>
                {
                    b.HasOne("Blog.Domain.AuthenticationContext.AccountAggregate.Account", null)
                        .WithOne()
                        .HasForeignKey("Blog.Domain.BlogContext.WriterAggregate.Writer", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlantsDetection.Models;

#nullable disable

namespace PlantsDetection.Data.Migrations
{
    [DbContext(typeof(PlantsDetectionContext))]
    [Migration("20240428191236_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlantsDetection.Models.Comment", b =>
                {
                    b.Property<int>("ComId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ComID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComId"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("NumOfComments")
                        .HasColumnType("int");

                    b.Property<int?>("NumOfDisLikes")
                        .HasColumnType("int");

                    b.Property<int?>("NumOfLikes")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("PostID");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ComId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.DetectDisease", b =>
                {
                    b.Property<int>("Did")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Did"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DiseaseType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ModelId")
                        .HasColumnType("int")
                        .HasColumnName("ModelID");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Did");

                    b.HasIndex("ModelId");

                    b.ToTable("DetectDisease", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.ModelImage", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ModelID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ModelType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ModelId");

                    b.HasIndex("UserId");

                    b.ToTable("ModelImage", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PostID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("NumOfComments")
                        .HasColumnType("int");

                    b.Property<int?>("NumOfDisLikes")
                        .HasColumnType("int");

                    b.Property<int?>("NumOfLikes")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("PostId");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Describtion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Report", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClientName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Token1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Token");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("PlantsDetection.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("StreetName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Token")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("UserDisLikeComment", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("ComId")
                        .HasColumnType("int")
                        .HasColumnName("ComID");

                    b.HasKey("UserId", "ComId");

                    b.HasIndex("ComId");

                    b.ToTable("UserDisLikeComment", (string)null);
                });

            modelBuilder.Entity("UserDisLikePost", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("PostID");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("UserDisLikePost", (string)null);
                });

            modelBuilder.Entity("UserLikeComment", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("ComId")
                        .HasColumnType("int")
                        .HasColumnName("ComID");

                    b.HasKey("UserId", "ComId");

                    b.HasIndex("ComId");

                    b.ToTable("UserLikeComment", (string)null);
                });

            modelBuilder.Entity("UserLikePost", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("PostID");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("UserLikePost", (string)null);
                });

            modelBuilder.Entity("PlantsDetection.Models.DetectDisease", b =>
                {
                    b.HasOne("PlantsDetection.Models.ModelImage", "Model")
                        .WithMany("DetectDiseases")
                        .HasForeignKey("ModelId")
                        .HasConstraintName("FK_DetectDisease_ModelImage");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("PlantsDetection.Models.ModelImage", b =>
                {
                    b.HasOne("PlantsDetection.Models.User", "User")
                        .WithMany("ModelImages")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ModelImage_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlantsDetection.Models.Report", b =>
                {
                    b.HasOne("PlantsDetection.Models.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Report_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlantsDetection.Models.Token", b =>
                {
                    b.HasOne("PlantsDetection.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Tokens_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserDisLikeComment", b =>
                {
                    b.HasOne("PlantsDetection.Models.Comment", null)
                        .WithMany()
                        .HasForeignKey("ComId")
                        .IsRequired()
                        .HasConstraintName("FK_UserDisLikeComment_Comment");

                    b.HasOne("PlantsDetection.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserDisLikeComment_User");
                });

            modelBuilder.Entity("UserDisLikePost", b =>
                {
                    b.HasOne("PlantsDetection.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .IsRequired()
                        .HasConstraintName("FK_UserDisLikePost_Post");

                    b.HasOne("PlantsDetection.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserDisLikePost_User");
                });

            modelBuilder.Entity("UserLikeComment", b =>
                {
                    b.HasOne("PlantsDetection.Models.Comment", null)
                        .WithMany()
                        .HasForeignKey("ComId")
                        .IsRequired()
                        .HasConstraintName("FK_UserLikeComment_Comment");

                    b.HasOne("PlantsDetection.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserLikeComment_User");
                });

            modelBuilder.Entity("UserLikePost", b =>
                {
                    b.HasOne("PlantsDetection.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .IsRequired()
                        .HasConstraintName("FK_UserLikePost_Post");

                    b.HasOne("PlantsDetection.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserLikePost_User");
                });

            modelBuilder.Entity("PlantsDetection.Models.ModelImage", b =>
                {
                    b.Navigation("DetectDiseases");
                });

            modelBuilder.Entity("PlantsDetection.Models.User", b =>
                {
                    b.Navigation("ModelImages");

                    b.Navigation("Reports");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}

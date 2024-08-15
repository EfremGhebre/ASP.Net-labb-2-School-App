﻿// <auto-generated />
using ASP.Net_labb_2_School_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASP.Net_labb_2_School_App.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20240728135426_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.CoursesStudent", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("CoursesStudents");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.CoursesTeacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("CoursesTeachers");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.CoursesStudent", b =>
                {
                    b.HasOne("ASP.Net_labb_2_School_App.Models.Course", "Course")
                        .WithMany("CoursesStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASP.Net_labb_2_School_App.Models.Student", "Student")
                        .WithMany("CoursesStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.CoursesTeacher", b =>
                {
                    b.HasOne("ASP.Net_labb_2_School_App.Models.Course", "Course")
                        .WithMany("CoursesTeachers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASP.Net_labb_2_School_App.Models.Teacher", "Teacher")
                        .WithMany("CoursesTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Student", b =>
                {
                    b.HasOne("ASP.Net_labb_2_School_App.Models.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Course", b =>
                {
                    b.Navigation("CoursesStudents");

                    b.Navigation("CoursesTeachers");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Student", b =>
                {
                    b.Navigation("CoursesStudents");
                });

            modelBuilder.Entity("ASP.Net_labb_2_School_App.Models.Teacher", b =>
                {
                    b.Navigation("CoursesTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}

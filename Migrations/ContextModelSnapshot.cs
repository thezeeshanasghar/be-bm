﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet.Models;

namespace dotnet.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("dotnet.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("ConsultationDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("DoctorId");

                    b.Property<int>("PatientId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("dotnet.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConsultationFee");

                    b.Property<int>("EmergencyConsultationFee");

                    b.Property<int>("ShareInFee");

                    b.Property<string>("SpecialityType");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("dotnet.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillType");

                    b.Property<string>("Category");

                    b.Property<string>("EmployeeOrVender");

                    b.Property<string>("Name");

                    b.Property<string>("PaymentType");

                    b.Property<double>("TotalBill");

                    b.Property<string>("TransactionDetail");

                    b.Property<int>("UserId");

                    b.Property<string>("VoucherNo");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("dotnet.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppointmentId");

                    b.Property<double>("CheckupFee");

                    b.Property<string>("CheckupType");

                    b.Property<DateTime>("Date");

                    b.Property<double>("Disposibles");

                    b.Property<int>("DoctorId");

                    b.Property<double>("GrossAmount");

                    b.Property<int>("PatientId");

                    b.Property<string>("PaymentType");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("dotnet.Models.InvoiceProcedures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvoiceId");

                    b.Property<int>("ProcedureId");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProcedureId");

                    b.ToTable("InvoiceProcedures");
                });

            modelBuilder.Entity("dotnet.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("dotnet.Models.Nurse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DutyDuration");

                    b.Property<double>("Salary");

                    b.Property<int>("SharePercentage");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("dotnet.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthPlace");

                    b.Property<string>("BloodGroup");

                    b.Property<string>("Category");

                    b.Property<string>("ClinicSite");

                    b.Property<string>("Description");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Guardian");

                    b.Property<string>("PaymentProfile");

                    b.Property<string>("ReferredBy");

                    b.Property<DateTime>("ReferredDate");

                    b.Property<string>("Type");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("dotnet.Models.Procedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Charges");

                    b.Property<string>("Executant");

                    b.Property<int>("ExecutantShare");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("dotnet.Models.Qualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Certificate");

                    b.Property<string>("Description");

                    b.Property<string>("QualificationType");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("dotnet.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Discount");

                    b.Property<int>("DoctorId");

                    b.Property<int>("InvoiceId");

                    b.Property<int>("PaidAmount");

                    b.Property<int>("PatientId");

                    b.Property<int>("PendingAmount");

                    b.Property<string>("Pmid");

                    b.Property<int>("ReceiptionistId");

                    b.Property<int>("TotalAmount");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("dotnet.Models.Receptionist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JobType");

                    b.Property<string>("ShiftTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Receptionists");
                });

            modelBuilder.Entity("dotnet.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("dotnet.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Cnic");

                    b.Property<string>("Contact");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("EmergencyContact");

                    b.Property<string>("Experience");

                    b.Property<string>("FatherHusbandName");

                    b.Property<string>("FirstName");

                    b.Property<int>("FloorNo");

                    b.Property<string>("Gender");

                    b.Property<DateTime>("JoiningDate");

                    b.Property<string>("LastName");

                    b.Property<string>("MaritalStatus");

                    b.Property<string>("Religion");

                    b.Property<string>("UserType");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("dotnet.Models.Appointment", b =>
                {
                    b.HasOne("dotnet.Models.Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Doctor", b =>
                {
                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Expense", b =>
                {
                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Invoice", b =>
                {
                    b.HasOne("dotnet.Models.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.InvoiceProcedures", b =>
                {
                    b.HasOne("dotnet.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet.Models.Procedure", "Procedures")
                        .WithMany()
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Nurse", b =>
                {
                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Patient", b =>
                {
                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Qualification", b =>
                {
                    b.HasOne("dotnet.Models.User", "user")
                        .WithMany("Qualifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Receipt", b =>
                {
                    b.HasOne("dotnet.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet.Models.Receptionist", b =>
                {
                    b.HasOne("dotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

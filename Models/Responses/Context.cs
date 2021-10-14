using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace dotnet.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<InvoiceProcedures> InvoiceProcedures { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Procedure>().Property(r => r.Consent).HasConversion(new BoolToZeroOneConverter<Int16>());
            modelBuilder.Entity<AppointmentDetail>().Property(r => r.HasDischarged).HasConversion(new BoolToZeroOneConverter<Int16>());
        }
    }
}
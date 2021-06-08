using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dotnet.Models {
    public class Context : DbContext {
        public Context (DbContextOptions<Context> options) : base (options) {

        }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Nurse> nurses { get; set; }
        public DbSet<Procedure> procedures { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<Qualifications> qualifications { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Appointment> appointments {get; set;}
        public DbSet<InvoiceProcedures> invoiceProcedures { get; set;}


    }
}
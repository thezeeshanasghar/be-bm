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
        public DbSet<Procedures> procedures { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Receipt> receipts { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Receiptionist> receiptionists { get; set; }
    

    }
}
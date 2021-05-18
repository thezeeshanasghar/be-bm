using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EmployeeType = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FatherHusbandName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    CNIC = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    EmergencyContact = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FlourNo = table.Column<int>(nullable: false),
                    Experience = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BillType = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    EmployeeOrVender = table.Column<string>(nullable: true),
                    VoucherNo = table.Column<string>(nullable: true),
                    ExpenseCategory = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    TotalBill = table.Column<double>(nullable: false),
                    TransactionDetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    FatherHusbandName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    LocalArea = table.Column<string>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    PatientDetails = table.Column<string>(nullable: true),
                    PlaceofBirth = table.Column<string>(nullable: true),
                    PatientCategory = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    PatientType = table.Column<string>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    ClinicSite = table.Column<string>(nullable: true),
                    ReferedBy = table.Column<string>(nullable: true),
                    ReferedDate = table.Column<DateTime>(nullable: false),
                    Religion = table.Column<string>(nullable: true),
                    PatientGardian = table.Column<string>(nullable: true),
                    PaymentProfile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "procedures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    PerformedBy = table.Column<string>(nullable: true),
                    Charges = table.Column<int>(nullable: false),
                    PerformerShare = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Availability = table.Column<string>(nullable: true),
                    FlourNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EmployeeId = table.Column<int>(nullable: false),
                    ConsultationFee = table.Column<int>(nullable: false),
                    EmergencyConsultationFee = table.Column<int>(nullable: false),
                    ShareInFee = table.Column<int>(nullable: false),
                    SpecialityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctors_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nurses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EmployeeId = table.Column<int>(nullable: false),
                    DutyDuration = table.Column<int>(nullable: false),
                    SharePercentage = table.Column<int>(nullable: false),
                    Salary = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nurses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nurses_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EmployeeId = table.Column<int>(nullable: false),
                    Certificate = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    qualificationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qualifications_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    PatientId = table.Column<int>(nullable: false),
                    PreviousVisitDate = table.Column<DateTime>(nullable: false),
                    TodayVisitDate = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    CheckupType = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    ProcedureId = table.Column<int>(nullable: false),
                    ProceduresId = table.Column<int>(nullable: true),
                    ConsultationFee = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    NetAmount = table.Column<double>(nullable: false),
                    Disposibles = table.Column<double>(nullable: false),
                    GrossAmount = table.Column<double>(nullable: false),
                    IsRefund = table.Column<int>(nullable: false),
                    RefundAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoices_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoices_procedures_ProceduresId",
                        column: x => x.ProceduresId,
                        principalTable: "procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    ReceiptionistId = table.Column<int>(nullable: false),
                    Pmid = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<long>(nullable: false),
                    PendingAmount = table.Column<long>(nullable: false),
                    PaidAmount = table.Column<long>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_receipts_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receipts_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receipts_payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctors_EmployeeId",
                table: "doctors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_DoctorId",
                table: "invoices",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_PatientId",
                table: "invoices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_ProceduresId",
                table: "invoices",
                column: "ProceduresId");

            migrationBuilder.CreateIndex(
                name: "IX_nurses_EmployeeId",
                table: "nurses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_qualifications_EmployeeId",
                table: "qualifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_DoctorId",
                table: "receipts",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_PatientId",
                table: "receipts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_PaymentId",
                table: "receipts",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "nurses");

            migrationBuilder.DropTable(
                name: "qualifications");

            migrationBuilder.DropTable(
                name: "receipts");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "procedures");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}

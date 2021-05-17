using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_receipts_payments_PaymentId",
                table: "receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_receipts_receiptionists_ReceiptionistId",
                table: "receipts");

            migrationBuilder.DropTable(
                name: "receiptionists");

            migrationBuilder.DropIndex(
                name: "IX_receipts_ReceiptionistId",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "EmergencyContact",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "doctors");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "receipts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicSite",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientCategory",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientGardian",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientType",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProfile",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceofBirth",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferedBy",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReferedDate",
                table: "patients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "patients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DutyDuration",
                table: "nurses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "nurses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "nurses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SharePercentage",
                table: "nurses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "doctors",
                nullable: false,
                defaultValue: 0);

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
                    FlourNo = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_nurses_EmployeeId",
                table: "nurses",
                column: "EmployeeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_employees_EmployeeId",
                table: "doctors",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_nurses_employees_EmployeeId",
                table: "nurses",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_payments_PaymentId",
                table: "receipts",
                column: "PaymentId",
                principalTable: "payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_employees_EmployeeId",
                table: "doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_nurses_employees_EmployeeId",
                table: "nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_receipts_payments_PaymentId",
                table: "receipts");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_nurses_EmployeeId",
                table: "nurses");

            migrationBuilder.DropIndex(
                name: "IX_doctors_EmployeeId",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ClinicSite",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PatientCategory",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PatientGardian",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PatientType",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PaymentProfile",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "PlaceofBirth",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ReferedBy",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ReferedDate",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "DutyDuration",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "SharePercentage",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "doctors");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "receipts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "nurses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "doctors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "receiptionists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FlourNo = table.Column<int>(nullable: false),
                    JobStatus = table.Column<string>(nullable: true),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receiptionists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_receipts_ReceiptionistId",
                table: "receipts",
                column: "ReceiptionistId");

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_payments_PaymentId",
                table: "receipts",
                column: "PaymentId",
                principalTable: "payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_receiptionists_ReceiptionistId",
                table: "receipts",
                column: "ReceiptionistId",
                principalTable: "receiptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

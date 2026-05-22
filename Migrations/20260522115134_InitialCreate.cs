using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Degree = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MinDegree = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Hrs = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrsResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrsResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrsResults_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrsResults_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Manager", "Name" },
                values: new object[,]
                {
                    { 1, "Dr. Ahmed Hassan", "Information Technology" },
                    { 2, "Dr. Sara Ali", "Computer Science" },
                    { 3, "Dr. Omar Khaled", "Software Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Degree", "DepartmentId", "Hrs", "MinDegree", "Name" },
                values: new object[,]
                {
                    { 1, 100m, 1, 3, 50m, "Windows Programming" },
                    { 2, 100m, 1, 3, 50m, "Database Systems" },
                    { 3, 100m, 2, 3, 50m, "Web Development" },
                    { 4, 100m, 2, 3, 50m, "Data Structures" },
                    { 5, 100m, 3, 3, 50m, "Software Design" }
                });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "Id", "Address", "DepartmentId", "Grade", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Alexandria", 1, "A", null, "Ali Hassan" },
                    { 2, "Cairo", 2, "B", null, "Nour Mohamed" },
                    { 3, "Mansoura", 1, "A", null, "Layla Ahmed" },
                    { 4, "Tanta", 3, "C", null, "Omar Ibrahim" }
                });

            migrationBuilder.InsertData(
                table: "CrsResults",
                columns: new[] { "Id", "CrsId", "Degree", "TraineeId" },
                values: new object[,]
                {
                    { 1, 1, 85m, 1 },
                    { 2, 2, 72m, 1 },
                    { 3, 3, 90m, 2 },
                    { 4, 4, 65m, 2 },
                    { 5, 1, 78m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Address", "CrsId", "DepartmentId", "Email", "ImageUrl", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "Alexandria, Egypt", 1, 1, "mahmoud@university.edu", null, "Mahmoud Alghonimy", 8000m },
                    { 2, "Cairo, Egypt", 3, 2, "sara@university.edu", null, "Sara Ali", 7500m },
                    { 3, "Giza, Egypt", 2, 1, "ahmed@university.edu", null, "Ahmed Mostafa", 9000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CrsResults_CrsId",
                table: "CrsResults",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_CrsResults_TraineeId",
                table: "CrsResults",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_CrsId",
                table: "Instructors",
                column: "CrsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_DepartmentId",
                table: "Trainees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrsResults");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

using System;
using MemoApp.Models.Object;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemoApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Memos",
                columns: table => new
                {
                    MemoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memos", x => x.MemoId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeJobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobs", x => new { x.JobId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_EmployeeJobs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoEmployees",
                columns: table => new
                {
                    MemoId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Signed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoEmployees", x => new { x.MemoId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_MemoEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoEmployees_Memos_MemoId",
                        column: x => x.MemoId,
                        principalTable: "Memos",
                        principalColumn: "MemoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoJobs",
                columns: table => new
                {
                    MemoId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoJobs", x => new { x.MemoId, x.JobId });
                    table.ForeignKey(
                        name: "FK_MemoJobs_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoJobs_Memos_MemoId",
                        column: x => x.MemoId,
                        principalTable: "Memos",
                        principalColumn: "MemoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobs_EmployeeId",
                table: "EmployeeJobs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoEmployees_EmployeeId",
                table: "MemoEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoJobs_JobId",
                table: "MemoJobs",
                column: "JobId");

            //Adding data to the migration
            //Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName", "Password" },
                values: new object[,]
                {
                    {0, "mod", "mod"},
                    {1, "math", "123"},
                    {2, "sarah", "456"},
                    {3, "sylv", "abc"},
                    {4, "joc", "def"},
                    {5, "anne", "qwe"},
                    {6, "pasc", "loi" },
                    {7, "rich", "roy" }
                }
                );

            //Employees
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Name", "UserId" },
                values: new object[,]
                {
                    {0, "Mathieu", 1},
                    {1, "Sarah-Maude", 2},
                    {2, "Sylvie", 3},
                    {3,"Jocelyn", 4},
                    {4, "Anne-Sophie", 5},
                    {5,"Pascal", 6},
                    {6, "Richard", 7}
                }
                );

            //Jobs
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Name" },
                values: new object[,]
                {
                    {0, "Production" },
                    {1, "Assistant production" },
                    {2, "Qualite" },
                    {3, "Bureaux" }
                }
                );

            //Memos
            migrationBuilder.InsertData(
                table: "Memos",
                columns: new[] { "MemoId", "Name", "Description", "CreationDate", "CreatedBy", "ModificationDate", "ModifiedBy" },
                values: new object[,]
                {
                    {0, "MEMO1", "Description memo 1",new DateTime(2024,08,23),"Sarah-Maude", null, null},
                    {1, "MEMO2", "Description memo 2",new DateTime(2024,08,25), "Pascal", null, null},
                    {2, "Memo3", "Description memo 3", new DateTime(2024,08,26),"Mathieu", null, null}
                }
                );

            //EmployeeJobs
            migrationBuilder.InsertData(
                table: "EmployeeJobs",
                columns: new[] { "JobId" , "EmployeeId" },
                values: new object[,]
                {
                    {0,5},
                    {0,6},
                    {1,3},
                    {2,1},
                    {2,5},
                    {3,0},
                    {3,2}
                }
                );

            //MemoJobs
            migrationBuilder.InsertData(
                table: "MemoJobs",
                columns : new[] { "MemoId", "JobId" },
                values : new object[,]
                {
                    {0,3},
                    {1,0},
                    {1,1},
                    {2,2}
                }
                );

            //MemoEmployees
            migrationBuilder.InsertData(
                table : "MemoEmployees",
                columns : new[] { "MemoId", "EmployeeId", "Signed" },
                values : new object[,]
                {
                    {0,0,false},
                    {0,2,true},
                    {1,5,true},
                    {1,6,false},
                    {1,3,true},
                    {2,1,false},
                    {2,5,false}
                }
                );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeJobs");

            migrationBuilder.DropTable(
                name: "MemoEmployees");

            migrationBuilder.DropTable(
                name: "MemoJobs");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Memos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodleClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RepositoryRenamedToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Repositories_RepositoryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Repositories_RepositoryId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.RenameColumn(
                name: "RepositoryId",
                table: "Assignments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_RepositoryId",
                table: "Assignments",
                newName: "IX_Assignments_CourseId");

            migrationBuilder.RenameColumn(
                name: "RepositoryId",
                table: "AspNetUsers",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_RepositoryId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CourseId");


            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Courses_CourseId",
                table: "AspNetUsers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Courses_CourseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Assignments",
                newName: "RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                newName: "IX_Assignments_RepositoryId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "AspNetUsers",
                newName: "RepositoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CourseId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_RepositoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositories_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_OwnerId",
                table: "Repositories",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Repositories_RepositoryId",
                table: "AspNetUsers",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Repositories_RepositoryId",
                table: "Assignments",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

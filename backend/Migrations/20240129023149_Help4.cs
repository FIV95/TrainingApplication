using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class Help4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_UserBase_UserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_UserBase_UserId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserBase_UserBaseId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase");

            migrationBuilder.RenameTable(
                name: "UserBase",
                newName: "UserBases");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserBases",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBases",
                table: "UserBases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBases_Email",
                table: "UserBases",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_UserBases_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_UserBases_UserId",
                table: "Coaches",
                column: "UserId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserBases_UserBaseId",
                table: "Comments",
                column: "UserBaseId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_UserBases_UserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_UserBases_UserId",
                table: "Coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserBases_UserBaseId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBases",
                table: "UserBases");

            migrationBuilder.DropIndex(
                name: "IX_UserBases_Email",
                table: "UserBases");

            migrationBuilder.RenameTable(
                name: "UserBases",
                newName: "UserBase");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserBase",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_UserBase_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "UserBase",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_UserBase_UserId",
                table: "Coaches",
                column: "UserId",
                principalTable: "UserBase",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserBase_UserBaseId",
                table: "Comments",
                column: "UserBaseId",
                principalTable: "UserBase",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class AddDiscriminatorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_UserBases_ClientId",
                table: "TrainingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_UserBases_CoachId",
                table: "TrainingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBases_UserBases_Client_UserBaseId",
                table: "UserBases");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBases_UserBases_CoachId",
                table: "UserBases");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBases_UserBases_UserBaseId",
                table: "UserBases");

            migrationBuilder.DropIndex(
                name: "IX_UserBases_Client_UserBaseId",
                table: "UserBases");

            migrationBuilder.DropIndex(
                name: "IX_UserBases_CoachId",
                table: "UserBases");

            migrationBuilder.DropIndex(
                name: "IX_UserBases_UserBaseId",
                table: "UserBases");

            migrationBuilder.DropColumn(
                name: "Client_UserBaseId",
                table: "UserBases");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "UserBases");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserBases");

            migrationBuilder.DropColumn(
                name: "UserBaseId",
                table: "UserBases");

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Coaches_UserBases_UserId",
                        column: x => x.UserId,
                        principalTable: "UserBases",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Clients_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Clients_UserBases_UserId",
                        column: x => x.UserId,
                        principalTable: "UserBases",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CoachId",
                table: "Clients",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Clients_ClientId",
                table: "TrainingSessions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Coaches_CoachId",
                table: "TrainingSessions",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Clients_ClientId",
                table: "TrainingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Coaches_CoachId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.AddColumn<int>(
                name: "Client_UserBaseId",
                table: "UserBases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "UserBases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserBases",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserBaseId",
                table: "UserBases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBases_Client_UserBaseId",
                table: "UserBases",
                column: "Client_UserBaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBases_CoachId",
                table: "UserBases",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBases_UserBaseId",
                table: "UserBases",
                column: "UserBaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_UserBases_ClientId",
                table: "TrainingSessions",
                column: "ClientId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_UserBases_CoachId",
                table: "TrainingSessions",
                column: "CoachId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBases_UserBases_Client_UserBaseId",
                table: "UserBases",
                column: "Client_UserBaseId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBases_UserBases_CoachId",
                table: "UserBases",
                column: "CoachId",
                principalTable: "UserBases",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBases_UserBases_UserBaseId",
                table: "UserBases",
                column: "UserBaseId",
                principalTable: "UserBases",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

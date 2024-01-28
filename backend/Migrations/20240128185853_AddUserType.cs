using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class AddUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionExerciseId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TrainingSessionExerciseId",
                table: "Comments",
                column: "TrainingSessionExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TrainingSessionId",
                table: "Comments",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TrainingSessionExercises_TrainingSessionExerciseId",
                table: "Comments",
                column: "TrainingSessionExerciseId",
                principalTable: "TrainingSessionExercises",
                principalColumn: "TrainingSessionExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_TrainingSessions_TrainingSessionId",
                table: "Comments",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "TrainingSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TrainingSessionExercises_TrainingSessionExerciseId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_TrainingSessions_TrainingSessionId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TrainingSessionExerciseId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TrainingSessionId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TrainingSessionExerciseId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "Comments");
        }
    }
}

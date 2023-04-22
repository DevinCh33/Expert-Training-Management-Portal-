using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Migrations
{
    /// <inheritdoc />
    public partial class xxxxxxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrainingStartDate",
                schema: "Identity",
                table: "Trainings",
                newName: "TrainingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "TrainingEndDate",
                schema: "Identity",
                table: "Trainings",
                newName: "TrainingEndDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrainingStartDateTime",
                schema: "Identity",
                table: "Trainings",
                newName: "TrainingStartDate");

            migrationBuilder.RenameColumn(
                name: "TrainingEndDateTime",
                schema: "Identity",
                table: "Trainings",
                newName: "TrainingEndDate");
        }
    }
}

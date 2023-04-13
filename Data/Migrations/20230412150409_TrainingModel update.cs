using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrainingModelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Trainings",
                newName: "TrainingVenue");

            migrationBuilder.AddColumn<bool>(
                name: "Availability",
                table: "Trainings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TrainingCategory",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrainingItinerary",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrainingName",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrainingPrice",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingCategory",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingItinerary",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingName",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingPrice",
                table: "Trainings");

            migrationBuilder.RenameColumn(
                name: "TrainingVenue",
                table: "Trainings",
                newName: "Name");
        }
    }
}

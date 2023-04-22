using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Migrations
{
    /// <inheritdoc />
    public partial class addstartdatetimeandenddatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingEndDateTime",
                schema: "Identity",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingStartDateTime",
                schema: "Identity",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingEndDateTime",
                schema: "Identity",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingStartDateTime",
                schema: "Identity",
                table: "Trainings");
        }
    }
}

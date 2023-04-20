using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Migrations
{
    /// <inheritdoc />
    public partial class paymentDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainings",
                schema: "Identity",
                table: "Trainings");

            migrationBuilder.RenameTable(
                name: "Trainings",
                schema: "Identity",
                newName: "TrainingModel",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingModel",
                schema: "Identity",
                table: "TrainingModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingModel",
                schema: "Identity",
                table: "TrainingModel");

            migrationBuilder.RenameTable(
                name: "TrainingModel",
                schema: "Identity",
                newName: "Trainings",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainings",
                schema: "Identity",
                table: "Trainings",
                column: "Id");
        }
    }
}

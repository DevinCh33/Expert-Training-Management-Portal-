using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainingImgURL",
                schema: "Identity",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingImgURL",
                schema: "Identity",
                table: "Trainings");
        }
    }
}

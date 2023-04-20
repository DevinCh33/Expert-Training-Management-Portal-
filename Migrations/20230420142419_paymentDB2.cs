using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETMP.Migrations
{
    /// <inheritdoc />
    public partial class paymentDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardNo = table.Column<int>(type: "int", nullable: false),
                    expiration = table.Column<int>(type: "int", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Identity");

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
    }
}

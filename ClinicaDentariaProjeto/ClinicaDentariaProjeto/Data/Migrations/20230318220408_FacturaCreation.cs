using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaDentariaProjeto.Data.Migrations
{
    /// <inheritdoc />
    public partial class FacturaCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberFactura = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceFactura = table.Column<float>(type: "real", nullable: false),
                    PaymentConfirme = table.Column<bool>(type: "bit", nullable: false),
                    ConsultaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Facturas_Consultas_ConsultaID",
                        column: x => x.ConsultaID,
                        principalTable: "Consultas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ConsultaID",
                table: "Facturas",
                column: "ConsultaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas");
        }
    }
}

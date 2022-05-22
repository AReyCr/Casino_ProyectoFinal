using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Casino_ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class Tarjetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RifaId = table.Column<int>(type: "int", nullable: false),
                    RifasId = table.Column<int>(type: "int", nullable: true),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    ParticipantesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Participantes_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "Participantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tarjetas_Rifas_RifasId",
                        column: x => x.RifasId,
                        principalTable: "Rifas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_ParticipantesId",
                table: "Tarjetas",
                column: "ParticipantesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_RifasId",
                table: "Tarjetas",
                column: "RifasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarjetas");
        }
    }
}

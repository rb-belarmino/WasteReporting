using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteReporting.API.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DestinosFinais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinosFinais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PontosColeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Responsavel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosColeta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recicladores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Categoria = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recicladores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residuos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residuos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coletas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PontoColetaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecicladorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DestinoFinalId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataColeta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coletas_DestinosFinais_DestinoFinalId",
                        column: x => x.DestinoFinalId,
                        principalTable: "DestinosFinais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coletas_PontosColeta_PontoColetaId",
                        column: x => x.PontoColetaId,
                        principalTable: "PontosColeta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coletas_Recicladores_RecicladorId",
                        column: x => x.RecicladorId,
                        principalTable: "Recicladores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColetaResiduos",
                columns: table => new
                {
                    ColetaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ResiduoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PesoKg = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColetaResiduos", x => new { x.ColetaId, x.ResiduoId });
                    table.ForeignKey(
                        name: "FK_ColetaResiduos_Coletas_ColetaId",
                        column: x => x.ColetaId,
                        principalTable: "Coletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColetaResiduos_Residuos_ResiduoId",
                        column: x => x.ResiduoId,
                        principalTable: "Residuos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColetaResiduos_ResiduoId",
                table: "ColetaResiduos",
                column: "ResiduoId");

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_DestinoFinalId",
                table: "Coletas",
                column: "DestinoFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_PontoColetaId",
                table: "Coletas",
                column: "PontoColetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_RecicladorId",
                table: "Coletas",
                column: "RecicladorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColetaResiduos");

            migrationBuilder.DropTable(
                name: "Coletas");

            migrationBuilder.DropTable(
                name: "Residuos");

            migrationBuilder.DropTable(
                name: "DestinosFinais");

            migrationBuilder.DropTable(
                name: "PontosColeta");

            migrationBuilder.DropTable(
                name: "Recicladores");
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Casino_ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class Participantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Rifas_RifasId",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "RifaId",
                table: "Participantes");

            migrationBuilder.AlterColumn<int>(
                name: "RifasId",
                table: "Participantes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Rifas_RifasId",
                table: "Participantes",
                column: "RifasId",
                principalTable: "Rifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Rifas_RifasId",
                table: "Participantes");

            migrationBuilder.AlterColumn<int>(
                name: "RifasId",
                table: "Participantes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RifaId",
                table: "Participantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Rifas_RifasId",
                table: "Participantes",
                column: "RifasId",
                principalTable: "Rifas",
                principalColumn: "Id");
        }
    }
}

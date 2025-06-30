using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace megaapi.Migrations
{
    /// <inheritdoc />
    public partial class FinalSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Idciudad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Idciudad);
                });

            migrationBuilder.CreateTable(
                name: "Paquete",
                columns: table => new
                {
                    Idpaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquete", x => x.Idpaquete);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    Idpromocion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alcance = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Vigencia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.Idpromocion);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Idservicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    PrecioBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Idservicio);
                });

            migrationBuilder.CreateTable(
                name: "Colonia",
                columns: table => new
                {
                    IdColonia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idciudad = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonia", x => x.IdColonia);
                    table.ForeignKey(
                        name: "FK_Colonia_Ciudad_Idciudad",
                        column: x => x.Idciudad,
                        principalTable: "Ciudad",
                        principalColumn: "Idciudad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocionCiudad",
                columns: table => new
                {
                    Idpromocion = table.Column<int>(type: "int", nullable: false),
                    Idciudad = table.Column<int>(type: "int", nullable: false),
                    PromocionIdpromocion = table.Column<int>(type: "int", nullable: false),
                    CiudadIdciudad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionCiudad", x => new { x.Idpromocion, x.Idciudad });
                    table.ForeignKey(
                        name: "FK_PromocionCiudad_Ciudad_CiudadIdciudad",
                        column: x => x.CiudadIdciudad,
                        principalTable: "Ciudad",
                        principalColumn: "Idciudad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocionCiudad_Promocion_PromocionIdpromocion",
                        column: x => x.PromocionIdpromocion,
                        principalTable: "Promocion",
                        principalColumn: "Idpromocion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocionPaquete",
                columns: table => new
                {
                    Idpromocion = table.Column<int>(type: "int", nullable: false),
                    Idpaquete = table.Column<int>(type: "int", nullable: false),
                    PromocionIdpromocion = table.Column<int>(type: "int", nullable: false),
                    PaqueteIdpaquete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionPaquete", x => new { x.Idpromocion, x.Idpaquete });
                    table.ForeignKey(
                        name: "FK_PromocionPaquete_Paquete_PaqueteIdpaquete",
                        column: x => x.PaqueteIdpaquete,
                        principalTable: "Paquete",
                        principalColumn: "Idpaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocionPaquete_Promocion_PromocionIdpromocion",
                        column: x => x.PromocionIdpromocion,
                        principalTable: "Promocion",
                        principalColumn: "Idpromocion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteServicio",
                columns: table => new
                {
                    Idpaquete = table.Column<int>(type: "int", nullable: false),
                    Idservicio = table.Column<int>(type: "int", nullable: false),
                    PaqueteIdpaquete = table.Column<int>(type: "int", nullable: false),
                    ServicioIdservicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteServicio", x => new { x.Idpaquete, x.Idservicio });
                    table.ForeignKey(
                        name: "FK_PaqueteServicio_Paquete_PaqueteIdpaquete",
                        column: x => x.PaqueteIdpaquete,
                        principalTable: "Paquete",
                        principalColumn: "Idpaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteServicio_Servicio_ServicioIdservicio",
                        column: x => x.ServicioIdservicio,
                        principalTable: "Servicio",
                        principalColumn: "Idservicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocionColonia",
                columns: table => new
                {
                    Idpromocion = table.Column<int>(type: "int", nullable: false),
                    Idcolonia = table.Column<int>(type: "int", nullable: false),
                    PromocionIdpromocion = table.Column<int>(type: "int", nullable: false),
                    ColoniaIdColonia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionColonia", x => new { x.Idpromocion, x.Idcolonia });
                    table.ForeignKey(
                        name: "FK_PromocionColonia_Colonia_ColoniaIdColonia",
                        column: x => x.ColoniaIdColonia,
                        principalTable: "Colonia",
                        principalColumn: "IdColonia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocionColonia_Promocion_PromocionIdpromocion",
                        column: x => x.PromocionIdpromocion,
                        principalTable: "Promocion",
                        principalColumn: "Idpromocion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suscriptor",
                columns: table => new
                {
                    Idsuscriptor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idcolonia = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscriptor", x => x.Idsuscriptor);
                    table.ForeignKey(
                        name: "FK_Suscriptor_Colonia_Idcolonia",
                        column: x => x.Idcolonia,
                        principalTable: "Colonia",
                        principalColumn: "IdColonia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Idcontrato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idsuscriptor = table.Column<int>(type: "int", nullable: false),
                    FechaContr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrecioBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Idcontrato);
                    table.ForeignKey(
                        name: "FK_Contrato_Suscriptor_Idsuscriptor",
                        column: x => x.Idsuscriptor,
                        principalTable: "Suscriptor",
                        principalColumn: "Idsuscriptor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratoPaquete",
                columns: table => new
                {
                    Idcontrato = table.Column<int>(type: "int", nullable: false),
                    Idpaquete = table.Column<int>(type: "int", nullable: false),
                    FechaAdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRetiro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContratoIdcontrato = table.Column<int>(type: "int", nullable: false),
                    PaqueteIdpaquete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoPaquete", x => new { x.Idcontrato, x.Idpaquete });
                    table.ForeignKey(
                        name: "FK_ContratoPaquete_Contrato_ContratoIdcontrato",
                        column: x => x.ContratoIdcontrato,
                        principalTable: "Contrato",
                        principalColumn: "Idcontrato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoPaquete_Paquete_PaqueteIdpaquete",
                        column: x => x.PaqueteIdpaquete,
                        principalTable: "Paquete",
                        principalColumn: "Idpaquete",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocionContrato",
                columns: table => new
                {
                    Idpromocion = table.Column<int>(type: "int", nullable: false),
                    Idcontrato = table.Column<int>(type: "int", nullable: false),
                    PromocionIdpromocion = table.Column<int>(type: "int", nullable: false),
                    ContratoIdcontrato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionContrato", x => new { x.Idpromocion, x.Idcontrato });
                    table.ForeignKey(
                        name: "FK_PromocionContrato_Contrato_ContratoIdcontrato",
                        column: x => x.ContratoIdcontrato,
                        principalTable: "Contrato",
                        principalColumn: "Idcontrato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocionContrato_Promocion_PromocionIdpromocion",
                        column: x => x.PromocionIdpromocion,
                        principalTable: "Promocion",
                        principalColumn: "Idpromocion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoPersonalizada",
                columns: table => new
                {
                    Idpromopersonalizada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idcontrato = table.Column<int>(type: "int", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioPorcen = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoPersonalizada", x => x.Idpromopersonalizada);
                    table.ForeignKey(
                        name: "FK_PromoPersonalizada_Contrato_Idcontrato",
                        column: x => x.Idcontrato,
                        principalTable: "Contrato",
                        principalColumn: "Idcontrato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colonia_Idciudad",
                table: "Colonia",
                column: "Idciudad");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_Idsuscriptor",
                table: "Contrato",
                column: "Idsuscriptor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContratoPaquete_ContratoIdcontrato",
                table: "ContratoPaquete",
                column: "ContratoIdcontrato");

            migrationBuilder.CreateIndex(
                name: "IX_ContratoPaquete_PaqueteIdpaquete",
                table: "ContratoPaquete",
                column: "PaqueteIdpaquete");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicio_PaqueteIdpaquete",
                table: "PaqueteServicio",
                column: "PaqueteIdpaquete");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicio_ServicioIdservicio",
                table: "PaqueteServicio",
                column: "ServicioIdservicio");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionCiudad_CiudadIdciudad",
                table: "PromocionCiudad",
                column: "CiudadIdciudad");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionCiudad_PromocionIdpromocion",
                table: "PromocionCiudad",
                column: "PromocionIdpromocion");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionColonia_ColoniaIdColonia",
                table: "PromocionColonia",
                column: "ColoniaIdColonia");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionColonia_PromocionIdpromocion",
                table: "PromocionColonia",
                column: "PromocionIdpromocion");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionContrato_ContratoIdcontrato",
                table: "PromocionContrato",
                column: "ContratoIdcontrato");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionContrato_PromocionIdpromocion",
                table: "PromocionContrato",
                column: "PromocionIdpromocion");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionPaquete_PaqueteIdpaquete",
                table: "PromocionPaquete",
                column: "PaqueteIdpaquete");

            migrationBuilder.CreateIndex(
                name: "IX_PromocionPaquete_PromocionIdpromocion",
                table: "PromocionPaquete",
                column: "PromocionIdpromocion");

            migrationBuilder.CreateIndex(
                name: "IX_PromoPersonalizada_Idcontrato",
                table: "PromoPersonalizada",
                column: "Idcontrato");

            migrationBuilder.CreateIndex(
                name: "IX_Suscriptor_Idcolonia",
                table: "Suscriptor",
                column: "Idcolonia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContratoPaquete");

            migrationBuilder.DropTable(
                name: "PaqueteServicio");

            migrationBuilder.DropTable(
                name: "PromocionCiudad");

            migrationBuilder.DropTable(
                name: "PromocionColonia");

            migrationBuilder.DropTable(
                name: "PromocionContrato");

            migrationBuilder.DropTable(
                name: "PromocionPaquete");

            migrationBuilder.DropTable(
                name: "PromoPersonalizada");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Paquete");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Suscriptor");

            migrationBuilder.DropTable(
                name: "Colonia");

            migrationBuilder.DropTable(
                name: "Ciudad");
        }
    }
}

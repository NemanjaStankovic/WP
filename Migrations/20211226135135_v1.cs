using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_projekat.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruktor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    GodinaRodjena = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruktor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    VrstaVozila = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaProizvodnje = table.Column<int>(type: "int", nullable: false),
                    ZapreminaMotora = table.Column<int>(type: "int", nullable: false),
                    SnagaMotora = table.Column<int>(type: "int", nullable: false),
                    VoziloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozilo_Vozilo_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozilo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstruktorVozilo",
                columns: table => new
                {
                    ListaInstruktoraID = table.Column<int>(type: "int", nullable: false),
                    VozilaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstruktorVozilo", x => new { x.ListaInstruktoraID, x.VozilaID });
                    table.ForeignKey(
                        name: "FK_InstruktorVozilo_Instruktor_ListaInstruktoraID",
                        column: x => x.ListaInstruktoraID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstruktorVozilo_Vozilo_VozilaID",
                        column: x => x.VozilaID,
                        principalTable: "Vozilo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polaznik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<int>(type: "int", maxLength: 13, nullable: false),
                    BrLicneKarte = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    PolozioTest = table.Column<bool>(type: "bit", nullable: false),
                    PolozioVoznju = table.Column<bool>(type: "bit", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VoziloID = table.Column<int>(type: "int", nullable: true),
                    InstruktorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaznik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Polaznik_Instruktor_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "Instruktor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polaznik_Vozilo_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozilo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstruktorVozilo_VozilaID",
                table: "InstruktorVozilo",
                column: "VozilaID");

            migrationBuilder.CreateIndex(
                name: "IX_Polaznik_InstruktorID",
                table: "Polaznik",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Polaznik_VoziloID",
                table: "Polaznik",
                column: "VoziloID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozilo_VoziloID",
                table: "Vozilo",
                column: "VoziloID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstruktorVozilo");

            migrationBuilder.DropTable(
                name: "Polaznik");

            migrationBuilder.DropTable(
                name: "Instruktor");

            migrationBuilder.DropTable(
                name: "Vozilo");
        }
    }
}

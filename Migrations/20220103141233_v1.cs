using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_projekat.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruktori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    GodinaRodjena = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<int>(type: "int", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruktori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RegistarskaTablica = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    VrstaVozila = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaProizvodnje = table.Column<int>(type: "int", nullable: false),
                    ZapreminaMotora = table.Column<int>(type: "int", nullable: false),
                    SnagaMotora = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Veza",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstruktorID = table.Column<int>(type: "int", nullable: true),
                    VoziloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veza", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Veza_Instruktori_InstruktorID",
                        column: x => x.InstruktorID,
                        principalTable: "Instruktori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veza_Vozilo_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozilo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polaznici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JMBG = table.Column<long>(type: "bigint", maxLength: 13, nullable: false),
                    BrLicneKarte = table.Column<int>(type: "int", maxLength: 9, nullable: false),
                    PolozioTest = table.Column<bool>(type: "bit", nullable: false),
                    PolozioVoznju = table.Column<bool>(type: "bit", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VezaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaznici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Polaznici_Veza_VezaID",
                        column: x => x.VezaID,
                        principalTable: "Veza",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polaznici_VezaID",
                table: "Polaznici",
                column: "VezaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veza_InstruktorID",
                table: "Veza",
                column: "InstruktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Veza_VoziloID",
                table: "Veza",
                column: "VoziloID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polaznici");

            migrationBuilder.DropTable(
                name: "Veza");

            migrationBuilder.DropTable(
                name: "Instruktori");

            migrationBuilder.DropTable(
                name: "Vozilo");
        }
    }
}

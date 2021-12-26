using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_projekat.Migrations
{
    public partial class v1novi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstruktorVozilo_Instruktor_ListaInstruktoraID",
                table: "InstruktorVozilo");

            migrationBuilder.DropForeignKey(
                name: "FK_Polaznik_Instruktor_InstruktorID",
                table: "Polaznik");

            migrationBuilder.DropForeignKey(
                name: "FK_Polaznik_Vozilo_VoziloID",
                table: "Polaznik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polaznik",
                table: "Polaznik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruktor",
                table: "Instruktor");

            migrationBuilder.RenameTable(
                name: "Polaznik",
                newName: "Polaznici");

            migrationBuilder.RenameTable(
                name: "Instruktor",
                newName: "Instruktori");

            migrationBuilder.RenameIndex(
                name: "IX_Polaznik_VoziloID",
                table: "Polaznici",
                newName: "IX_Polaznici_VoziloID");

            migrationBuilder.RenameIndex(
                name: "IX_Polaznik_InstruktorID",
                table: "Polaznici",
                newName: "IX_Polaznici_InstruktorID");

            migrationBuilder.AlterColumn<long>(
                name: "JMBG",
                table: "Polaznici",
                type: "bigint",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 13);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polaznici",
                table: "Polaznici",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruktori",
                table: "Instruktori",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_InstruktorVozilo_Instruktori_ListaInstruktoraID",
                table: "InstruktorVozilo",
                column: "ListaInstruktoraID",
                principalTable: "Instruktori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznici_Instruktori_InstruktorID",
                table: "Polaznici",
                column: "InstruktorID",
                principalTable: "Instruktori",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznici_Vozilo_VoziloID",
                table: "Polaznici",
                column: "VoziloID",
                principalTable: "Vozilo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstruktorVozilo_Instruktori_ListaInstruktoraID",
                table: "InstruktorVozilo");

            migrationBuilder.DropForeignKey(
                name: "FK_Polaznici_Instruktori_InstruktorID",
                table: "Polaznici");

            migrationBuilder.DropForeignKey(
                name: "FK_Polaznici_Vozilo_VoziloID",
                table: "Polaznici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polaznici",
                table: "Polaznici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruktori",
                table: "Instruktori");

            migrationBuilder.RenameTable(
                name: "Polaznici",
                newName: "Polaznik");

            migrationBuilder.RenameTable(
                name: "Instruktori",
                newName: "Instruktor");

            migrationBuilder.RenameIndex(
                name: "IX_Polaznici_VoziloID",
                table: "Polaznik",
                newName: "IX_Polaznik_VoziloID");

            migrationBuilder.RenameIndex(
                name: "IX_Polaznici_InstruktorID",
                table: "Polaznik",
                newName: "IX_Polaznik_InstruktorID");

            migrationBuilder.AlterColumn<int>(
                name: "JMBG",
                table: "Polaznik",
                type: "int",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 13);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polaznik",
                table: "Polaznik",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruktor",
                table: "Instruktor",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_InstruktorVozilo_Instruktor_ListaInstruktoraID",
                table: "InstruktorVozilo",
                column: "ListaInstruktoraID",
                principalTable: "Instruktor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznik_Instruktor_InstruktorID",
                table: "Polaznik",
                column: "InstruktorID",
                principalTable: "Instruktor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Polaznik_Vozilo_VoziloID",
                table: "Polaznik",
                column: "VoziloID",
                principalTable: "Vozilo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

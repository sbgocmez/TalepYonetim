using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalepYonetim.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AltKategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltKategoriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AltKategoriler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talepler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    Edenİsim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EdenSoyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltKategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talepler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talepler_AltKategoriler_AltKategoriId",
                        column: x => x.AltKategoriId,
                        principalTable: "AltKategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltKategoriler_KategoriId",
                table: "AltKategoriler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Talepler_AltKategoriId",
                table: "Talepler",
                column: "AltKategoriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talepler");

            migrationBuilder.DropTable(
                name: "AltKategoriler");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
